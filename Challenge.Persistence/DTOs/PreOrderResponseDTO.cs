using System;
using Challenge.Persistence.ResponseDatas;

namespace Challenge.Persistence.DTOs
{
    public class PreOrderResponseDTO
    {
        public PreOrderResponseData Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
} 