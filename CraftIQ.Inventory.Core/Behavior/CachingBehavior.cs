using CraftIQ.Inventory.Core.Cache;
using CraftIQ.Inventory.Core.ICachingServices;
using MediatR;

namespace CraftIQ.Inventory.Core.ValidationBehavior
{

    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICacheable
    {
        private readonly ICacheService _cache;

        public CachingBehavior(ICacheService cache)
        {
            _cache = cache;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request.BypassCache)
                return await next();

            var cached = await _cache.GetAsync<TResponse>(request.CacheKey);

            if (cached != null)
                return cached;

            var response = await next();

            await _cache.SetAsync(
                request.CacheKey,
                response,
                TimeSpan.FromMinutes(5));

            return response;
        }
    }
}
