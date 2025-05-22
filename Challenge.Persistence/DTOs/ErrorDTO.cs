using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Challenge.Persistence.DTOs
{
    public record ErrorDTO
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Message { get; set; }
    }
}
