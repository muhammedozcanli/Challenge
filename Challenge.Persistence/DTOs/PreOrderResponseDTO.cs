using System;
using System.Collections.Generic;

namespace Challenge.Persistence.DTOs
{
    public class PreOrderResponseDTO
    {
        public PreOrderResponseData Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }

    public class PreOrderResponseData
    {
        public double Amount { get; set; }
        public List<PreOrderProductResponseDTO> Products { get; set; }
    }

    public class PreOrderProductResponseDTO
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
} 