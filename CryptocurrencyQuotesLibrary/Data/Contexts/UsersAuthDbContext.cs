using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptocurrencyQuotesLibrary.Data.Contexts
{
    public class UsersAuthDbContext : IdentityDbContext
    {
        public UsersAuthDbContext(DbContextOptions<UsersAuthDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
