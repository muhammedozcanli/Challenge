using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Persistence.DTOs
{
    public record PreOrderDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int Amount { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? Status { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime? CancelledAt { get; set; }
    }
}
