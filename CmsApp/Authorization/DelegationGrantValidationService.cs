// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using DAL.Core.Interfaces;
using DAL.Models;
using IdentityServer.ExtensionGrant.Delegation.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using CmsApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsApp.Authorization
{
    public class DelegationGrantValidationService : GrantValidationService<ApplicationUser, String>
    {
        private readonly IAccountManager _accountManager;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IEmailSender _emailSender;
        private readonly string _publicRoleName;

        public DelegationGrantValidationService(UserManager<ApplicationUser> userManager, IAccountManager accountManager,
            IHttpContextAccessor httpContextAccessor, IEmailSender emailSender, IOptions<AppSettings> appSettings)
            : base(userManager)
        {
            _accountManager = accountManager;
            _httpContext = httpContextAccessor;
            _emailSender = emailSender;
            _publicRoleName = appSettings?.Value.DefaultUserRole;
        }

        public override async Task<ApplicationUser> CreateUserAsync(string username, string email)
        {
            var user = await base.CreateUserAsync(username, email);
            user.IsEnabled = true;

            var result = await _accountManager.UpdateUserAsync(user, new[] { _publicRoleName });
            if (result.Succeeded)
                await SendVerificationEmail(user);

            return user;
        }

        private async Task SendVerificationEmail(ApplicationUser appUser)
        {
            string code = await _accountManager.GenerateEmailConfirmationTokenAsync(appUser);
            string callbackUrl = EmailTemplates.GetConfirmEmailCallbackUrl(_httpContext.HttpContext.Request, appUser.Id, code);
            string message = EmailTemplates.GetConfirmAccountEmail(appUser.UserName, callbackUrl);

            await _emailSender.SendEmailAsync(appUser.UserName, appUser.Email, "Confirm your email", message);
        }
    }
}
