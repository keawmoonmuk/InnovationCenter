using DAL.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models.Identity {

    public class AppIdentityRole : IdentityRole, AppIAuditableEntity {

        //*******overload********for******appIdentityRole
        public AppIdentityRole()
        {

        }

        public AppIdentityRole(string roleName) : base(roleName)
        {

        }

        public AppIdentityRole(string roleName, string description) : base(roleName)
        {
            Description = description;
        }

        /// <summary>
        /// Gets or sets the description for this role.
        /// </summary>
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// Navigation property for the users in this role.
        /// </summary>
        public virtual ICollection<IdentityUserRole<string>> Users { get; set; }

        /// <summary>
        /// Navigation property for claims in this role.
        /// </summary>
        public virtual ICollection<IdentityRoleClaim<string>> Claims { get; set; }

    }
}
