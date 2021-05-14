using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using BookStoreStore.Infrastructure.Data;
using BookStoreStore.Infrastructure.Data.Initializer;
using BS.Domain.Entities;


namespace BookStoreStore.Infrastructure
{
    public static class DependencyInjection
    {
        public static readonly LoggerFactory _myLoggerFactory =
         new LoggerFactory(new[] {
     new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()
         });

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options =>

                options.UseLoggerFactory(_myLoggerFactory).UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                serverOptions =>
                {
                    serverOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                }));



            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            services.AddScoped<IRepositoryFactory, UnitOfWork<ApplicationDbContext>>();
            //services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            services.AddScoped<IDbInitializer, DbInitializer>();

            return services;
        }
     
    }
}
