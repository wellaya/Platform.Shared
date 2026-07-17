using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Platform.Application.Common.Interfaces;

namespace Platform.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest>(
    ILogger<LoggingBehaviour<TRequest>> logger, ICurrentUser currentUser)
    : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling {RequestName} for user {UserId}",
            typeof(TRequest).Name, currentUser.Id ?? "anonymous");
        return Task.CompletedTask;
    }
}