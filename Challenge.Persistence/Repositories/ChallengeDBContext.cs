using Challenge.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Persistence.Repositories
{
    public class ChallengeDBContext : DbContext
    {
        private readonly string _connectionString;
        public ChallengeDBContext(DbContextOptions<ChallengeDBContext> options)
        : base(options)
        {
        }

        public virtual DbSet<Balance> Balances { get; set; }
        public virtual DbSet<Error> Errors { get; set; }
        public virtual DbSet<PreOrder> PreOrders { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Balance
            List<Balance> balanceSeed = new List<Balance>
            {
                new Balance
                {
                    Id = new Guid("00000000-aaaa-bbbb-cccc-111111111111"),
                    UserId = Guid.Parse("550e8400-e29b-41d4-a716-446655440000"),
                    AvailableBalance = 10,
                    BlockedBalance = 0,
                    Currency = "USD",
                    LastUpdated = DateTime.Parse("2023-06-15T10:30:00Z", null, System.Globalization.DateTimeStyles.AdjustToUniversal)
                },
            };
            #endregion

            #region Product

            List<Product> productSeed = new List<Product>
            {
                new Product
                {
                    Id = new Guid("a1b2c3d4-e5f6-4111-aaaa-111111111111"),
                    Name = "Premium Smartphone",
                    Description = "Latest model with advanced features",
                    Price = 19.99,
                    Currency = "USD",
                    Category = "Electronics",
                    Stock = 42
                },
                new Product
                {
                    Id = new Guid("b2c3d4e5-f6a7-4222-bbbb-222222222222"),
                    Name = "Wireless Headphones",
                    Description = "Noise-cancelling with premium sound quality",
                    Price = 14.99,
                    Currency = "USD",
                    Category = "Electronics",
                    Stock = 78
                },
                new Product
                {
                    Id = new Guid("c3d4e5f6-a7b8-4333-cccc-333333333333"),
                    Name = "Smart Watch",
                    Description = "Fitness tracking and notifications",
                    Price = 12.99,
                    Currency = "USD",
                    Category = "Electronics",
                    Stock = 0
                },
                new Product
                {
                    Id = new Guid("d4e5f6a7-b8c9-4444-dddd-444444444444"),
                    Name = "Laptop",
                    Description = "High-performance for work and gaming",
                    Price = 19.99,
                    Currency = "USD",
                    Category = "Electronics",
                    Stock = 15
                },
                new Product
                {
                    Id = new Guid("e5f6a7b8-c9d0-4555-eeee-555555555555"),
                    Name = "Wireless Charger",
                    Description = "Fast charging for compatible devices",
                    Price = 9.99,
                    Currency = "USD",
                    Category = "Accessories",
                    Stock = 120
                }
            };

            #endregion

            modelBuilder.Entity<Balance>().HasData(balanceSeed);
            modelBuilder.Entity<Product>().HasData(productSeed);

            base.OnModelCreating(modelBuilder);
        }
    }
}
