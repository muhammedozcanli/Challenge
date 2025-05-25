using System.ComponentModel.DataAnnotations;

namespace Challenge.Persistence.DTOs
{
    public class PreOrderProductDTO
    {
        public Guid ProductId { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public int Quantity { get; set; }
    }
}
