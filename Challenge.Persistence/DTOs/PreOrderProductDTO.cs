using System.ComponentModel.DataAnnotations;

namespace Challenge.Persistence.DTOs
{
    public class PreOrderProductDTO
    {
        [Required]
        public Guid ProductId { get; set; }
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public int Quantity { get; set; }
    }
}
