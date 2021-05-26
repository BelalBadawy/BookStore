using System;
using System.Reflection;
using System.Security.Claims;
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

        private IHttpContextAccessor _httpContextAccessor;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IHttpContextAccessor httpContextAccessor,
            IDateTimeService dateTime
        ) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
            _httpContextAccessor = httpContextAccessor;

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

            string userId = _httpContextAccessor?.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.NowUtc;
                        entry.Entity.CreatedBy = (string.IsNullOrEmpty(userId) ? null : Guid.Parse(userId));
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.NowUtc;
                        entry.Entity.LastModifiedBy = (string.IsNullOrEmpty(userId) ? null : Guid.Parse(userId));
                        break;
                }
            }
            try
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException e)
            {
                //This either returns a error string, or null if it can’t handle that error
                var sqlException = e.GetBaseException();
                if (sqlException != null)
                {
                    throw new ApplicationException(sqlException.Message, sqlException.InnerException); //return the error string
                }
                throw new ApplicationException("couldn’t handle that error"); //return the error string
                //couldn’t handle that error, so rethrow
            }
        }

    }
}
