using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResturangDB_API.Models
{
    public class Booking
    {
        [Key]
        public int BookingID { get; set; }

        [ForeignKey("Table")]
        public int FK_TableID { get; set; }
        public Table? Table { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required]
        public DateTime TimeEnd { get; set; }

        public required string Name { get; set; }

        public required string Email { get; set; }

        public required string PhoneNumber { get; set; }
    }
}
