using Microsoft.EntityFrameworkCore;
using Platform.Domain.Common;

namespace Platform.Infrastructure.Persistence;

public abstract class BasePlatformDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Ignore<BaseEvent>();
        base.OnModelCreating(builder);
    }
}