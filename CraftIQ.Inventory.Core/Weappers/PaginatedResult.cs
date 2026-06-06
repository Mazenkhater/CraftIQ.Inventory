using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftIQ.Inventory.Core.Weappers
{
    public class PaginatedResult<T>
    {
        //public IEnumerable<T> Items { get; set; }

        public T Items { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasNextPage => PageNumber < TotalPages;
        public bool HasPreviousPage => PageNumber > 1;
        public PaginatedResult(T items, int totalcount, int pagenumber, int pagesize)
        {
            Items = items;
            TotalCount = totalcount;
            PageNumber = pagenumber;
            PageSize = pagesize;
        }

    }
}
