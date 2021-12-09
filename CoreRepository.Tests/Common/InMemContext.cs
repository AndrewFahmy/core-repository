using CoreRepository.Tests.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreRepository.Tests.Common
{
    public class InMemContext : DbContext
    {
        public InMemContext(DbContextOptions options) : base(options) { }


        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Product> Products { get; set; } = null!;

        public DbSet<Basket> Baskets { get; set; } = null!;

        public DbSet<History> HistoryRecords { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Basket>().HasKey(k => new { k.UserId, k.ProductId });

            builder.Entity<History>().HasKey(k => new { k.UserId, k.ProductId });
        }
    }
}
