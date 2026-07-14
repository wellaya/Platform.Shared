using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;
using Xunit;

namespace Platform.Testing;

/// <summary>
/// Base class for integration tests. Spins up a real SQL Server in a container.
/// Vertical test projects derive a concrete class supplying their own DbContext type.
/// </summary>
public abstract class BaseIntegrationTest<TContext> : IAsyncLifetime where TContext : DbContext
{
    private readonly MsSqlContainer _container = new MsSqlBuilder().Build();
    protected TContext DbContext { get; private set; } = default!;

    public async Task InitializeAsync()
    {
        await _container.StartAsync();
        DbContext = CreateContext(_container.GetConnectionString());
        await DbContext.Database.MigrateAsync();
    }

    protected abstract TContext CreateContext(string connectionString);

    public async Task DisposeAsync()
    {
        await DbContext.DisposeAsync();
        await _container.DisposeAsync();
    }
}