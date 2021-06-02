using System.Threading.Tasks;
using BS.Domain.Common;
using Microsoft.AspNetCore.Authorization;

namespace BS.Infrastructure.Identity
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
       
        public PermissionAuthorizationHandler()
        {
        }

        #region From db Context

        //UserManager<ApplicationUser> _userManager;
        //RoleManager<IdentityRole<Guid>> _roleManager;

        //public PermissionAuthorizationHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        //{
        //    _userManager = userManager;
        //    _roleManager = roleManager;
        //}

        //protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        //{
        //    if (context.User == null)
        //    {
        //        return;
        //    }

        //    // Get all the roles the user belongs to and check if any of the roles has the permission required
        //    // for the authorization to succeed.
        //    var user = await _userManager.GetUserAsync(context.User);
        //    if (user == null)
        //    {
        //        return;
        //    }
        //    var userRoleNames = await _userManager.GetRolesAsync(user);
        //    var userRoles = _roleManager.Roles.Where(x => userRoleNames.Contains(x.Name));

        //    foreach (var role in userRoles)
        //    {
        //        var roleClaims = await _roleManager.GetClaimsAsync(role);
        //        var permissions = roleClaims.Where(x => x.Type == CustomClaimTypes.Permission &&
        //                                                x.Value == requirement.Permission &&
        //                                                x.Issuer == "LOCAL AUTHORITY")
        //            .Select(x => x.Value);

                //        if (permissions.Any())
                //        {
                //            context.Succeed(requirement);
                //            return;
                //        }
                //    }
                //}

                #endregion


                #region From Token





        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }

            // If user does not have the scope claim, get out of here
            if (context.User.HasClaim(c => c.Type == CustomClaimTypes.Permission &&
                                           c.Value == requirement.Permission.ToUpper() 
                                         //  && c.Issuer == "http://localhost:55445"
                                           ))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
        #endregion
    }
}
       
