using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace ShopsRUs.Api.Models
{
    public class ShopsRUsDbContext : DbContext
    {
        public ShopsRUsDbContext()
        {
        }

        public ShopsRUsDbContext(DbContextOptions<ShopsRUsDbContext> options)
         : base(options) { }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Discount> Discounts => Set<Discount>();
        public DbSet<UsedDiscount> UsedDiscounts => Set<UsedDiscount>();
      
    }
}

