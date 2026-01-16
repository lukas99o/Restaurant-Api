using System.ComponentModel.DataAnnotations;

namespace ResturangDB_API.Models
{
    public class Table
    {
        [Key]
        public int TableID { get; set; }

        [Required]
        public int TableSeats { get; set; }

        [Required]
        public bool IsAvailable { get; set; } 

        public ICollection<Booking> Bookings { get; set; }
    }
}
