using Challenge.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //#region Balance
            //List<Balance> balanceSeed = new List<Balance>
            //{
            //    new Balance
            //    {
            //        Id = Guid.NewGuid(),
            //        UserId = 1,
            //        AvailableBalance = 10,
            //        BlockedBalance = 0,
            //        Currency = "USD",
            //        LastUpdated = DateTime.UtcNow,
                    
                    
            //    },
            //    new Balance
            //    {
            //        Id = Guid.NewGuid(),
            //        UserId = 2,
            //        AvailableBalance = 20,
            //        BlockedBalance = 0,
            //        Currency = "USD",
            //        LastUpdated = DateTime.UtcNow,
            //    },
            //};
            //#endregion

            //#region Error
            //List<Error> errorSeed = new List<Error>
            //{
            //    new Error
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Error1"
            //    },
            //    new Error
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Error2"
            //    },
            //};
            //#endregion

            //#region PreOrder
            //List<PreOrder> preOrderSeed = new List<PreOrder>
            //{
            //    new PreOrder
            //    {
            //        Id = Guid.NewGuid(),
            //        OrderId = 1,
            //        Amount = 10,
            //        CreatedDate = DateTime.UtcNow.AddMinutes(-5),
            //        CompletedAt = DateTime.UtcNow,
            //        Status = "Completed"
            //    },
            //    new PreOrder
            //    {
            //        Id = Guid.NewGuid(),
            //        OrderId = 2,
            //        Amount = 5,
            //        CancelledAt = DateTime.UtcNow,
            //        CreatedDate = DateTime.UtcNow.AddMinutes(-5),
            //        Status = "Cancelled"

            //    },
            //};
            //#endregion

            //#region Product

            //List<Product> productSeed = new List<Product>
            //{
            //    new Product
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Product1",
            //        Description = "Product1 description",
            //        Category = "Product1 Category",
            //        Price = 10,
            //        Stock = 50
            //    },
            //    new Product
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Product2",
            //        Description = "Product2 description",
            //        Category = "Product2 Category",
            //        Price = 5,
            //        Stock = 70
            //    },
            // };

            //#endregion

            //modelBuilder.Entity<Balance>().HasData(balanceSeed);
            //modelBuilder.Entity<Error>().HasData(errorSeed);
            //modelBuilder.Entity<PreOrder>().HasData(preOrderSeed);
            //modelBuilder.Entity<Product>().HasData(productSeed);
        }
    }
}
