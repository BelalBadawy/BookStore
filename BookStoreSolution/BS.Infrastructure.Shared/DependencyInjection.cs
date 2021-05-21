using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BS.Application.Interfaces;
using BS.Infrastructure.Shared.Services;

namespace BS.Infrastructure.Shared
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IDateTimeService, DateTimeService>();
            return services;
        }
    }
}
