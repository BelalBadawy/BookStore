using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BS.Application.Interfaces;
using BS.Infrastructure.Identity.Services;

namespace BS.Infrastructure.Identity
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services)
        {

            services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();

            return services;
        }
    }
}
