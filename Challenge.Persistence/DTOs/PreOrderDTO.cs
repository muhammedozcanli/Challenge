using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Challenge.Persistence.DTOs
{
    public class PreOrderDTO
    {
        [Required]
        [MinLength(1, ErrorMessage = "At least one product is required.")]
        public List<PreOrderProductDTO> Products { get; set; }
    }
}
