using System;
using BS.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BS.Infrastructure.Data.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async void Initialize()
        {
            //try
            //{
            //    if (_db.Database.GetPendingMigrations().Count() > 0)
            //    {
            //        _db.Database.Migrate();
            //    }
            //}
            //catch (Exception ex)
            //{

            //}

            //AppClaimsInitializer.AppClaimsAsync(_db);
            UserInitializer.AddUser(_db, _userManager, _roleManager);

        }



    }

}


