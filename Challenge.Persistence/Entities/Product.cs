using Challenge.Persistance.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Persistence.Entities
{
    public class Product : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public string? Currency { get; set; }
        public string? Category { get; set; }
        public int? Stock { get; set; }
    }
}
