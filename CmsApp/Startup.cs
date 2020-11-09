// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using AutoMapper;
using DAL;
using DAL.Core;
using DAL.Core.Interfaces;
using DAL.Models;
using IdentityServer.ExtensionGrant.Delegation.Services;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CmsApp.Authorization;
using CmsApp.Controllers;
using CmsApp.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;
using AppPermissions = DAL.Core.ApplicationPermissions;
using Microsoft.Extensions.Options;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;    //mysql 
using DAL.DATA;
using DAL.Models.Identity;
using System.Diagnostics;   //debug

namespace CmsApp
{
    public class Startup
    {
        private IWebHostEnvironment _env { get; }
        public IConfiguration Configuration { get; }


        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _env = env;
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];     //"Server=localhost;Database=CmsApp;Uid=root;Password=mdt1234;"
            string migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;  //cmsapp

            Debug.WriteLine("ConnectionString => " + connectionString);
            Debug.WriteLine("MigrationsAssembly =>" + migrationsAssembly);
            ////***********connect Sqlserver******************
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(connectionString, b => b.MigrationsAssembly(migrationsAssembly)));


            //services.AddDbContextPool<ApplicationDbContext>(options => options
            //    // replace with your connection string
            //    .UseMySql("Server=localhost;Database=cmsapp;User=root;Password=mdt1234;", mySqlOptions => mySqlOptions
            //        // replace with your Server Version and Type
            //        .ServerVersion(new Version(8, 0, 18), ServerType.MySql)));

            //*********connect mysql server*************
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseMySql(connectionString, b => 
               { b.MigrationsAssembly(migrationsAssembly); b.ServerVersion(new Version(8, 0), ServerType.MySql); }));
 

            // add identity
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Configure Identity options and password complexity here
            services.Configure<IdentityOptions>(options =>
            {
                // User settings
                options.User.RequireUniqueEmail = true;

                //    //// Password settings
                //    //options.Password.RequireDigit = true;
                //    //options.Password.RequiredLength = 8;
                //    //options.Password.RequireNonAlphanumeric = false;
                //    //options.Password.RequireUppercase = true;
                //    //options.Password.RequireLowercase = false;

                //    //// Lockout settings
                //    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                //    //options.Lockout.MaxFailedAccessAttempts = 10;
            });


            // Adds IdentityServer.
            services.AddIdentityServer()
              // The AddDeveloperSigningCredential extension creates temporary key material for signing tokens.
              // This might be useful to get started, but needs to be replaced by some persistent key material for production scenarios.
              // See http://docs.identityserver.io/en/release/topics/crypto.html#refcrypto for more information.

              .AddDeveloperSigningCredential()
              .AddConfigurationStore(options =>
              {
                  //user sql server
                  //options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
                  options.ConfigureDbContext = builder => builder.UseMySql(connectionString, mysql => mysql.MigrationsAssembly(migrationsAssembly));
              })
              .AddOperationalStore(options =>
              {
                  //user sql server
                  //options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
                  options.ConfigureDbContext = builder => builder.UseMySql(connectionString, mysql => mysql.MigrationsAssembly(migrationsAssembly));

                  // this enables automatic token cleanup. this is optional. 
                  options.EnableTokenCleanup = true;
                  options.TokenCleanupInterval = 30;
              })
              .AddAspNetIdentity<ApplicationUser>()
              .AddProfileService<ProfileService>()
              .AddDelegationGrant<ApplicationUser, String>()
              .AddDefaultSocialLoginValidators(options =>
              {
                  options.TwitterConsumerAPIKey = Configuration["Twitter:ConsumerAPIKey"];
                  options.TwitterConsumerSecret = Configuration["Twitter:ConsumerSecret"];
              });

            var applicationUrl = Configuration["ApplicationUrl"].TrimEnd('/');   //http://localhost:5050
            Debug.WriteLine("Application URL => " + applicationUrl);

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = applicationUrl;
                    options.SupportedTokens = SupportedTokens.Jwt;
                    options.RequireHttpsMetadata = false; // Note: Set to true in production
                    options.ApiName = IdentityServerConfig.ApiName;

                    Debug.WriteLine("options authority  =>" + options.Authority);
                    Debug.WriteLine("options supported tokens =>" + options.SupportedTokens);
                    Debug.WriteLine("options api name" + options.ApiName);
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Authorization.Policies.ViewAllUsersPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, AppPermissions.ViewUsers));
                options.AddPolicy(Authorization.Policies.ManageAllUsersPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, AppPermissions.ManageUsers));

                options.AddPolicy(Authorization.Policies.ViewAllRolesPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, AppPermissions.ViewRoles));
                options.AddPolicy(Authorization.Policies.ViewRoleByRoleNamePolicy, policy => policy.Requirements.Add(new ViewRoleAuthorizationRequirement()));
                options.AddPolicy(Authorization.Policies.ManageAllRolesPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, AppPermissions.ManageRoles));

                options.AddPolicy(Authorization.Policies.AssignAllowedRolesPolicy, policy => policy.Requirements.Add(new AssignRolesAuthorizationRequirement()));
            });


            // Add cors
            services.AddCors();

            services.AddControllersWithViews();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            //*********************Add Service Swagger**********************
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = IdentityServerConfig.ApiFriendlyName, Version = "v1" });   //Config Swagger Document
                c.OperationFilter<AuthorizeCheckOperationFilter>();      // Authorize Check Operation Filter  
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri("/connect/token", UriKind.Relative),
                            Scopes = new Dictionary<string, string>()
                            {
                                { IdentityServerConfig.ApiName, IdentityServerConfig.ApiFriendlyName }
                            }
                        }
                    }
                });
            });

            services.AddAutoMapper(typeof(Startup));

            // Configurations
            services.Configure<AppSettings>(Configuration);


            // Business Services
            services.AddScoped<IEmailSender, EmailSender>();


            // Repositories
           // services.AddScoped<IUnitOfWork, HttpUnitOfWork>();
            services.AddScoped<IAccountManager, AccountManager>();

            // Auth Handlers
            services.AddSingleton<IAuthorizationHandler, ViewUserAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ManageUserAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ViewRoleAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, AssignRolesAuthorizationHandler>();

            // DB Creation and Seeding
            services.AddTransient<IDatabaseInitializer, AppDbInitializer>();

            // Delegation Grant
            services.AddScoped<IGrantValidationService, DelegationGrantValidationService>();

            //Http Clients
            services.AddHttpClient<TwitterController>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            Utilities.ConfigureLogger(loggerFactory);
            EmailTemplates.Initialize(env);

            //help insert data otp...
            //InitializeDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.UseIdentityServer();
            app.UseAuthorization();

            //********************Config Swagger Api Document*******************************
            app.UseSwagger();
            app.UseSwaggerUI( c =>
            {
                c.DocumentTitle = "Swagger UI - CmsApp";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{IdentityServerConfig.ApiFriendlyName} V1");
                c.OAuthClientId(IdentityServerConfig.SwaggerClientID);
                c.OAuthClientSecret("no_password"); //Leaving it blank doesn't work
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";
    
                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                    spa.Options.StartupTimeout = TimeSpan.FromSeconds(600); // Increase the timeout if angular app is taking longer to startup
                    //spa.UseProxyToSpaDevelopmentServer("http://localhost:4200"); // Use this instead to use the angular cli server
                }
            });
        }


    }
}
