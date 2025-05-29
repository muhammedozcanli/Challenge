using Challenge.Persistence.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace Challenge.API.Examples.Requests
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
                        Quantity = 2
                    }
                }
            };
        }
    }
}
