using CraftIQ.Inventory.Core.Features.Categories.Queries.Models;
using CraftIQ.Inventory.Core.ICachingServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftIQ.Inventory.Core.CachingBehavior
{
    public class CacheBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICacheableQuery
    {
        private readonly IRedisCacheService _cache;

        public CacheBehavior(IRedisCacheService cache)
        {
            _cache = cache;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var cachedData = await _cache.GetAsync<TResponse>(request.CacheKey);

            if (cachedData is not null)
            {
                return cachedData;
            }

            var response = await next();

            await _cache.SetAsync(request.CacheKey,response,request.Expiration);

            return response;
        }
    }
}
