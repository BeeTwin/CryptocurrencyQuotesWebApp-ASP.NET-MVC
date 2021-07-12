using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyQuotesLibrary.Utils.Extentions
{
    public static class DbSetExtentions
    {
        public static async Task AddRangeIfNotExistsAsync<T>(this DbSet<T> dbSet, List<T> entities) where T : class, new()
        {
            entities = entities
                .Where(x => !dbSet.Contains(x))
                .ToList();
            await dbSet.AddRangeAsync(entities);
        }
    }
}
