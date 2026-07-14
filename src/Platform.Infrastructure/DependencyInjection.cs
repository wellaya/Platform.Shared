using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Platform.Infrastructure.Persistence;
using Platform.Infrastructure.Persistence.Interceptors;

namespace Platform.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddPlatformInfrastructureServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IVerticalConnectionStringResolver, ConfigurationConnectionStringResolver>();
        services.AddScoped<AuditableEntityInterceptor>();
        return services;
    }
}