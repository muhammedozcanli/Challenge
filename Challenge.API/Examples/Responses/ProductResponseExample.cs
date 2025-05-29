using Challenge.Common.Utilities.Result.Concrete;
using Challenge.Persistence.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace Challenge.API.Examples.Responses
{
    public class ProductResponseExample : IExamplesProvider<List<ProductDTO>>
    {
        public List<ProductDTO> GetExamples()
        {
            return new List<ProductDTO>
            {
                new ProductDTO
                {
                    Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                    Name = "Wireless Headphones",
                    Description = "Noise-cancelling with premium sound quality",
                    Price = 9.99,
                    Currency = "USD",
                    Category = "Electronics",
                    Stock = 52
                },
                new ProductDTO
                {
                    Id = Guid.Parse("a1b2c3d4-e5f6-4111-aaaa-111111111111"),
                    Name = "Premium Smartphone",
                    Description = "Latest model with the advanced features",
                    Price = 49.99,
                    Currency = "USD",
                    Category = "Electronics",
                    Stock = 999
                },

            };
        }
    }
}
