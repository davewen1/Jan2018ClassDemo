using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using AppSecurity.DAL;
using AppSecurity.Entities;
#endregion



namespace AppSecurity.BLL
{
    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        private ApplicationUserManager UserManager { get; set; }
        public ApplicationRoleManager() :
            base(new RoleStore<IdentityRole>(new ApplicationDbContext()))
        {
            //needed to add this to get UserManager in ListAllRoles() to have a value.
            UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }
        public ApplicationRoleManager(ApplicationUserManager userManager) :
            base(new RoleStore<IdentityRole>(new ApplicationDbContext()))
        {
            UserManager = userManager;
        }
        public void AddDefaultRoles()
        {
            foreach (string roleName in SecurityRoles.DefaultSecurityRoles)
            {
                // Check if it exists
                if (!Roles.Any(r => r.Name == roleName))
                {
                    this.Create(new IdentityRole(roleName));
                }
            }
        }
    }

}
