using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;
using Platform.Application.Common.Exceptions;
using System.Net;

namespace Platform.Functions.Middleware;

public sealed class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    : IFunctionsWorkerMiddleware
{
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException vex)
        {
            await WriteError(context, HttpStatusCode.BadRequest, "Validation failed", vex.Errors);
        }
        catch (NotFoundException nfex)
        {
            await WriteError(context, HttpStatusCode.NotFound, nfex.Message, null);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception in {FunctionName}", context.FunctionDefinition.Name);
            await WriteError(context, HttpStatusCode.InternalServerError, "An unexpected error occurred.", null);
        }
    }

    private static async Task WriteError(FunctionContext context, HttpStatusCode status, string title, object? errors)
    {
        var req = await context.GetHttpRequestDataAsync();
        if (req is null) return;

        var res = req.CreateResponse(status);
        await res.WriteAsJsonAsync(new { title, status = (int)status, errors });
        context.GetInvocationResult().Value = res;
    }
}