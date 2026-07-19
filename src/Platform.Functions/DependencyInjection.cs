using Microsoft.Extensions.DependencyInjection;
using Platform.Application.Common.Interfaces;
using Platform.Functions.Services;

namespace Platform.Functions;

public static class DependencyInjection
{
    public static IServiceCollection AddPlatformFunctionsServices(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUser, FunctionsCurrentUser>();
        return services;
    }
}