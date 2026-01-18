using ResturangDB_API.Migrations;

namespace ResturangDB_API.Models
{
    public class User
    {
        public int UserID { get; set; }
        public required string Username { get; set; }
        public required string PasswordHash { get; set; } 
        public bool ChangedPassword { get; set; } = false;
    }
}
