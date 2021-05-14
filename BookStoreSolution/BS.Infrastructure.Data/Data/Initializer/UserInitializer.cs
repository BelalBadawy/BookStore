
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using BS.Domain.Entities;

namespace BookStoreStore.Infrastructure.Data.Initializer
{
    public class UserInitializer
    {
        public static void AddUser(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            //if (!db.Roles.Any(r => r.Name == "Admin"))
            //{
            //    roleManager.CreateAsync(new IdentityRole<Guid>("Admin")).GetAwaiter().GetResult();
            //}

            //if (!db.Roles.Any(r => r.Name == "User"))
            //{
            //    roleManager.CreateAsync(new IdentityRole<Guid>("User")).GetAwaiter().GetResult();
            //}


            //if (!db.ApplicationUsers.Any(u => u.Email == "admin@gmail.com"))
            //{
            //    userManager.CreateAsync(new ApplicationUser
            //    {
            //        UserName = "Belal",
            //        Email = "admin@gmail.com",
            //        EmailConfirmed = true,



            //    }, "Admin123$").GetAwaiter().GetResult();

            //    ApplicationUser user = db.ApplicationUsers.Where(u => u.Email == "admin@gmail.com").FirstOrDefault();

            //    var newRoleName = "Admin";

            //    roleManager.CreateAsync(new IdentityRole<Guid>(newRoleName)).GetAwaiter().GetResult();

            //    var newRole = roleManager.FindByNameAsync(newRoleName).GetAwaiter().GetResult();

            //    //if (newRole != null)
            //    //{
            //    //    List<AppClaim> existsAppClaims = new List<AppClaim>();

            //    //    existsAppClaims = db.AppClaim.ToListAsync().GetAwaiter().GetResult();

            //    //    var claims = roleManager.GetClaimsAsync(newRole).GetAwaiter().GetResult();

            //    //    foreach (var ca in existsAppClaims)
            //    //    {
            //    //        if (!string.IsNullOrEmpty(ca.ClaimTitle))
            //    //        {
            //    //            if (!claims.Any(o => o.Value.ToUpper() == ca.ClaimTitle.ToUpper()))
            //    //            {
            //    //                roleManager.AddClaimAsync(newRole, new Claim("permission", ca.ClaimTitle.ToUpper())).GetAwaiter().GetResult();
            //    //            }
            //    //        }
            //    //    }
            //    //}

            //    userManager.AddToRoleAsync(user, newRoleName).GetAwaiter().GetResult();
            //}
        }
    }
}


