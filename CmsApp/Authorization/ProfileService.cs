// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using DAL.Core;
using DAL.Models;

namespace CmsApp.Authorization
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;

        public ProfileService(UserManager<ApplicationUser> userManager, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }

        //get profile async data  
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            var principal = await _claimsFactory.CreateAsync(user);

            Console.WriteLine("sub =>"+ sub);
            Console.WriteLine("user =>" + user);
            Console.WriteLine("principal => " + principal);

            var claims = principal.Claims.ToList();
            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();

            Console.WriteLine("Claims => "+ claims);
            //jobTitle
            if (user.JobTitle != null)
                claims.Add(new Claim(PropertyConstants.JobTitle, user.JobTitle));

            //fullname
            if (user.FullName != null)
                claims.Add(new Claim(PropertyConstants.FullName, user.FullName));

            //configuration
            if (user.Configuration != null)
                claims.Add(new Claim(PropertyConstants.Configuration, user.Configuration));

            context.IssuedClaims = claims;
        }


        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);

            Console.WriteLine("IsActiveAsyne for sub  => " + sub);
            Console.WriteLine("IsActiveAsyne for user => " + user);

            context.IsActive = (user != null) && user.IsEnabled;
        }
    }
}