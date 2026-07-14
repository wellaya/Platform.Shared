using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Platform.Application.Common.Behaviours;

namespace Platform.Application;

public static class DependencyInjection
{
    /// <summary>
    /// Registers MediatR handlers, validators, and pipeline behaviours found in the given assembly
    /// (pass the calling vertical's Application assembly).
    /// </summary>
    public static IServiceCollection AddPlatformApplicationServices(
        this IServiceCollection services, Assembly verticalApplicationAssembly)
    {
        services.AddValidatorsFromAssembly(verticalApplicationAssembly);

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(verticalApplicationAssembly);
            cfg.AddOpenRequestPreProcessor(typeof(LoggingBehaviour<,>));
            cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            cfg.AddOpenBehavior(typeof(PerformanceBehaviour<,>));
        });

        return services;
    }
}