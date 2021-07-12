using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyQuotesLibrary.Utils
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList(IEnumerable<T> items, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(items.Count() / (double)pageSize);
            items = items
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);

            AddRange(items);
        }

        public bool HasPreviousPage()
        {
            return (PageIndex > 1);
        }

        public bool HasNextPage()
        {
            return (PageIndex < TotalPages);
        }       
    }
}
