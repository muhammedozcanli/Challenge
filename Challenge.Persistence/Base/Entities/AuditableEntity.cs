using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Persistance.Base.Entities
{
    public abstract class AuditableEntity : BaseEntity
    {
        public AuditableEntity() {
            CreatedDate = DateTime.UtcNow;
        }
        public DateTime CreatedDate { get; set; }

    }
}
