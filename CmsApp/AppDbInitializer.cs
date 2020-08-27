// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;             //import Logging
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Core;
using DAL.Core.Interfaces;
using IdentityServer4.EntityFramework.DbContexts;  //import IdentityServer4 EntityFramwork DbContext
using DAL;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.Extensions.Options;

namespace CmsApp
 {
    public class AppDbInitializer : DatabaseInitializer {
        private readonly PersistedGrantDbContext _persistedGrantContext;
        private readonly ConfigurationDbContext _configurationContext;
        private readonly ILogger _logger;


        public AppDbInitializer(
            ApplicationDbContext context,
            PersistedGrantDbContext persistedGrantContext,
            ConfigurationDbContext configurationContext,
            IAccountManager accountManager,
            ILogger<AppDbInitializer> logger,
            IOptions<AppSettings> appSettings) : base(context, accountManager, logger, appSettings.Value.DefaultUserRole)
        {
            _persistedGrantContext = persistedGrantContext;
            _configurationContext = configurationContext;
            _logger = logger;
        }


        override public async Task SeedAsync()
        {
            await base.SeedAsync().ConfigureAwait(false);
            await _persistedGrantContext.Database.MigrateAsync().ConfigureAwait(false);
            await _configurationContext.Database.MigrateAsync().ConfigureAwait(false);

            if (!await _configurationContext.Clients.AnyAsync()) {
                _logger.LogInformation("Seeding IdentityServer Clients");

                foreach (var client in IdentityServerConfig.GetClients()) {
                    _configurationContext.Clients.Add(client.ToEntity());
                }

                _configurationContext.SaveChanges();
            }

            if (!await _configurationContext.IdentityResources.AnyAsync()) {
                _logger.LogInformation("Seeding IdentityServer Identity Resources");

                foreach (var resource in IdentityServerConfig.GetIdentityResources()) {
                    _configurationContext.IdentityResources.Add(resource.ToEntity());
                }

                _configurationContext.SaveChanges();
            }

            if (!await _configurationContext.ApiResources.AnyAsync()) {
                _logger.LogInformation("Seeding IdentityServer API Resources");

                foreach (var resource in IdentityServerConfig.GetApiResources()) {
                    _configurationContext.ApiResources.Add(resource.ToEntity());
                }

                _configurationContext.SaveChanges();
            }
        }
    }
}