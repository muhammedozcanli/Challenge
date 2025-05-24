using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Persistence.DTOs
{
    public record UserDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
