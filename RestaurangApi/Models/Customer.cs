using System.ComponentModel.DataAnnotations;

namespace ResturangDB_API.Models
{
    public class Customer
    {
        [Key] 
        public int CustomerID { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Password { get; set; } 

        public ICollection<Booking> Bookings { get; set; }
    }
}
