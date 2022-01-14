using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore.Dao;
using NetCore.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMember2Service(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<IMember2Service, Member2Service>(x =>
            {
                var ConnectionStrings = Configuration.GetValue<string>("ConnectionStrings:netCore");
                return new Member2Service(new Member2Dao(ConnectionStrings));
            });

            return services;
        }

    }
}
