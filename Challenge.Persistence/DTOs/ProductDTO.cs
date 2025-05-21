using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Persistence.DTOs
{
    public record ProductDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Price { get; set; }
        public string? Category { get; set; }
        public DateTime? LastUpdated { get; set; }
        public int? Stock { get; set; }
    }
}
