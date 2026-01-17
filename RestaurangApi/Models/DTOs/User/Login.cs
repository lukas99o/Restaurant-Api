using System.ComponentModel.DataAnnotations;

namespace ResturangDB_API.Models.DTOs.User
{
    public class Login
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
