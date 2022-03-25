using iParty.Api.Infra;
using iParty.Business.Infra;
using iParty.Data.Infra;
using iParty.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace iParty.Api.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddCustomDependencyInjection(this IServiceCollection services)
        {
            services.Scan(scan => scan.FromAssembliesOf(typeof(BusinessInjection), typeof(DataInjection), typeof(ApiInjection))
                                      .AddClasses()
                                      .AsImplementedInterfaces()
                                      .WithScopedLifetime());

            services.AddSingleton<DatabaseConfig>();

            return services;
        }
    }
}
