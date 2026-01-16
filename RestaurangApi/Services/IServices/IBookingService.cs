using ResturangDB_API.Models.DTOs.Booking;

namespace ResturangDB_API.Services.IServices
{
    public interface IBookingService
    {
        Task AddBookingAsync(BookingCreateDTO booking);
        Task<IEnumerable<BookingGetDTO>> GetAllBookingsAsync();
        Task<BookingGetDTO> GetBookingByIdAsync(int bookingID);
        Task<bool> UpdateBookingAsync(BookingUpdateDTO booking);
        Task<bool> DeleteBookingAsync(int bookingID);
    }
}
