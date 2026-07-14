using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Hosting;
using Platform.Functions.Middleware;

namespace Platform.Functions.Extensions;

public static class FunctionsBuilderExtensions
{
    public static FunctionsApplicationBuilder UsePlatformMiddleware(this FunctionsApplicationBuilder builder)
    {
        builder.UseMiddleware<CorrelationIdMiddleware>();
        builder.UseMiddleware<ExceptionHandlingMiddleware>();
        return builder;
    }
}