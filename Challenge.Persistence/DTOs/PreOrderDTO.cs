using System;
using System.ComponentModel.DataAnnotations;

namespace Challenge.Persistence.DTOs
{
    public record PreOrderDTO
    {
        [Required]
        public Guid OrderId { get; set; }
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public int Amount { get; set; }
    }
}
