namespace CraftIQ.Inventory.Core.Cache
{
    public interface ICacheable
    {
        string CacheKey { get; }

        //int SlidingExpirationInMinutes { get; }

        TimeSpan Expiration { get; }

        bool BypassCache { get; }
    }
}
