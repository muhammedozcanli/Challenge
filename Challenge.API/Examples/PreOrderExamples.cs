using Challenge.Persistence.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace Challenge.API.Examples
{
    public class PreOrderRequestExample : IExamplesProvider<PreOrderDTO>
    {
        public PreOrderDTO GetExamples()
        {
            return new PreOrderDTO
            {
                Products = new List<PreOrderProductDTO>
                {
                    new PreOrderProductDTO
                    {
                        ProductId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                        Quantity = 2147483647
                    }
                }
            };
        }
    }

    public class PreOrderResponseExample : IExamplesProvider<PreOrderResponseDTO>
    {
        public PreOrderResponseDTO GetExamples()
        {
            return new PreOrderResponseDTO
            {
                Data = new PreOrderResponseData
                {
                    Amount = 19.99,
                    Products = new List<PreOrderProductResponseDTO>
                    {
                        new PreOrderProductResponseDTO
                        {
                            ProductId = "a1b2c3d4-e5f6-4111-aaaa-111111111111",
                            Quantity = 1
                        }
                    }
                },
                Success = true,
                Message = "Pre-order created successfully.",
                StatusCode = 200
            };
        }
    }
} 