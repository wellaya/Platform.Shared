using Microsoft.EntityFrameworkCore;
using Platform.Application.Common.Models;

namespace Platform.Infrastructure.Persistence.Extensions;

public static class QueryableExtensions
{
    public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(
        this IQueryable<T> source, int pageNumber, int pageSize, CancellationToken ct = default)
    {
        var count = await source.CountAsync(ct);
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(ct);
        return new PaginatedList<T>(items, count, pageNumber, pageSize);
    }
}