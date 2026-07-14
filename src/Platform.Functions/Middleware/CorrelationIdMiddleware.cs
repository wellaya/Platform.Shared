using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;

namespace Platform.Functions.Middleware;

public sealed class CorrelationIdMiddleware : IFunctionsWorkerMiddleware
{
    private const string HeaderName = "X-Correlation-Id";

    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        var req = await context.GetHttpRequestDataAsync();
        var correlationId = req?.Headers.TryGetValues(HeaderName, out var values) == true
            ? values.First()
            : Guid.NewGuid().ToString();

        context.Items["CorrelationId"] = correlationId;
        await next(context);
    }
}