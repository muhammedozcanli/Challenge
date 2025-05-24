using System.ComponentModel.DataAnnotations;

namespace Challenge.Persistence.DTOs
{
    public record LoginDTO
    {
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
} 