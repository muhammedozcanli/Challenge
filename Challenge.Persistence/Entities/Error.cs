using Challenge.Persistance.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Persistence.Entities
{
    public class Error : BaseEntity
    {
        public string? Name { get; set; }
        public string? Message { get; set; }
    }
}
