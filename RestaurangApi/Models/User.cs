using ResturangDB_API.Migrations;

namespace ResturangDB_API.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string? Username { get; set; }
        public string PasswordHash { get; set; } = "admin";
    }
}
