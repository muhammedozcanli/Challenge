using Challenge.Persistance.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Persistence.Entities
{
    public class PreOrder : AuditableEntity
    {
        public Guid OrderId { get; set; }
        public int Amount { get; set; }
        public string? Status { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime? CancelledAt { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

        public ICollection<PreOrderProduct> PreOrderProducts { get; set; } = new List<PreOrderProduct>();
    }
}
