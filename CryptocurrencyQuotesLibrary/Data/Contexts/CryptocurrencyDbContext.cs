using CryptocurrencyQuotesLibrary.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptocurrencyQuotesLibrary.Data.Contexts
{
    public class CryptocurrencyDbContext : DbContext
    {
        public DbSet<Cryptocurrency> Cryptocurrency { get; set; }

        public CryptocurrencyDbContext(DbContextOptions<CryptocurrencyDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
