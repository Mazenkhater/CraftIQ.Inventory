using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftIQ.Inventory.Core.Features.Categories.Queries.Models
{
    public interface ICacheableQuery
    {
        string CacheKey { get; }

        TimeSpan Expiration { get; }
    }
}
