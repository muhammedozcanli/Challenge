    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
