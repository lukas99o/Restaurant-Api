using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ResturangDB_API.Models.DTOs.Booking
{
    public class BookingCreateDTO
    {
        public int TableID { get; set; }
        public DateTime Time { get; set; }
        public DateTime TimeEnd { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
