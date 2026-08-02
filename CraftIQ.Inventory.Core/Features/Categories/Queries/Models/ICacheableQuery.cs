namespace CraftIQ.Inventory.Core.Features.Categories.Queries.Models
{
    public interface ICacheableQuery
    {
        string CacheKey { get; }

        TimeSpan Expiration { get; }
    }
}
