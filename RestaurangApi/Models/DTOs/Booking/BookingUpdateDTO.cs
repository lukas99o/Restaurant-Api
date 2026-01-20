namespace ResturangDB_API.Models.DTOs.Booking
{
    public class BookingUpdateDTO
    {
        public int BookingID { get; set; }
        public int TableID { get; set; }
        public DateTime Time { get; set; }
        public DateTime TimeEnd { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
    }
}
