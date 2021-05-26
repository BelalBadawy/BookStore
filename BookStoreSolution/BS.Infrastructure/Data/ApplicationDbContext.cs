using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using BS.Application.Interfaces;
using BS.Domain.Common;
using BS.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BS.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        private readonly IDateTimeService _dateTime;
        private readonly IAuthService _authenticatedUser;
        private IHttpContextAccessor _httpContextAccessor;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options, 
            IHttpContextAccessor httpContextAccessor, 
            IDateTimeService dateTime, 
            IAuthService authenticatedUser
            ) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _authenticatedUser = authenticatedUser;
        }

        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<BookCategory> BookCategory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfiguration(new BookCategoryConfiguration());

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {

            #region OLD


            ////// Get all the entities that inherit from BaseEntity
            ////// and have a state of Added or Modified
            ////var entries = ChangeTracker
            ////    .Entries()
            ////    .Where(e => e.Entity is BaseEntity && (
            ////        e.State == EntityState.Added
            ////        || e.State == EntityState.Modified));

            ////// For each entity we will set the Audit properties
            ////foreach (var entityEntry in entries)
            ////{
            ////    // If the entity state is Added let's set
            ////    // the CreatedAt and CreatedBy properties
            ////    if (entityEntry.State == EntityState.Added)
            ////    {
            ////        ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
            ////        if (_httpContextAccessor.HttpContext.User != null && _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            ////        {
            ////            ((BaseEntity)entityEntry.Entity).CreatedBy = Guid.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            ////        }
            ////    }
            ////    else
            ////    {
            ////        // If the state is Modified then we don't want
            ////        // to modify the CreatedAt and CreatedBy properties
            ////        // so we set their state as IsModified to false
            ////        Entry((BaseEntity)entityEntry.Entity).Property(p => p.CreatedDate).IsModified = false;
            ////        Entry((BaseEntity)entityEntry.Entity).Property(p => p.CreatedBy).IsModified = false;

            ////        // In any case we always want to set the properties
            ////        // ModifiedAt and ModifiedBy
            ////        ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.Now;
            ////        if (_httpContextAccessor.HttpContext.User != null &&
            ////            _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            ////        {
            ////            ((BaseEntity)entityEntry.Entity).UpdatedBy = Guid.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            ////        }
            ////    }


            ////}

            ////// After we set all the needed properties
            ////// we call the base implementation of SaveChangesAsync
            ////// to actually save our entities in the database

            //return await base.SaveChangesAsync(cancellationToken);

            #endregion

            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.NowUtc;
                        entry.Entity.CreatedBy = _authenticatedUser.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.NowUtc;
                        entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
