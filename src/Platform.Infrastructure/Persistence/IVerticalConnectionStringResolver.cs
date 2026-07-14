using Microsoft.Extensions.Configuration;

namespace Platform.Infrastructure.Persistence;

public interface IVerticalConnectionStringResolver
{
    string GetConnectionString(string verticalName);
}

public sealed class ConfigurationConnectionStringResolver(IConfiguration configuration)
    : IVerticalConnectionStringResolver
{
    public string GetConnectionString(string verticalName)
    {
        var dedicated = configuration[$"ConnectionStrings:{verticalName}"];
        if (!string.IsNullOrWhiteSpace(dedicated)) return dedicated;

        var shared = configuration["ConnectionStrings:SharedPlatformDb"];
        if (!string.IsNullOrWhiteSpace(shared)) return shared;

        throw new InvalidOperationException(
            $"No connection string configured for '{verticalName}' and no 'SharedPlatformDb' fallback found. " +
            $"Set 'ConnectionStrings:{verticalName}' to give this vertical its own database, " +
            $"or 'ConnectionStrings:SharedPlatformDb' to share one across verticals.");
    }
}