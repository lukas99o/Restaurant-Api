using ResturangDB_API.Models;

namespace ResturangDB_API.Data.Repos.IRepos
{
    public interface IBookingRepo
    {
        Task AddBookingAsync(Booking booking);
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<Booking> GetBookingByIDAsync(int bookingID);
        Task UpdateBookingAsync(Booking booking);
        Task DeleteBookingAsync(Booking booking);
    }
}
