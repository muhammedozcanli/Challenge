using Challenge.Persistence.DTOs;
using Challenge.Persistence.ResponseDatas;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;

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
                        Quantity = 2
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
                    Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                    Amount = 19.99
                },
                Success = true,
                Message = "Pre-order created successfully.",
                StatusCode = 200
            };
        }
    }
} 