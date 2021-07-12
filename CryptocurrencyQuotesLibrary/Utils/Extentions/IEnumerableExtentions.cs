using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CryptocurrencyQuotesLibrary.Utils.Extentions
{
    public static class IEnumerableExtentions
    {
        public static IOrderedEnumerable<T> OrderBy<T, U>(
            this IEnumerable<T> list, 
            bool isDescending, 
            Func<T, U> selector)
        {
            if (isDescending)
            {
                return list.OrderByDescending(selector);
            }
            else
            {
                return list.OrderBy(selector);
            }
        }
    }
}
