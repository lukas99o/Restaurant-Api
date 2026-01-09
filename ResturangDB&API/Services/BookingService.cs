using ResturangDB_API.Data.Repos.IRepos;
using ResturangDB_API.Models;
using ResturangDB_API.Services.IServices;
using Microsoft.AspNetCore.Http.HttpResults;
using ResturangDB_API.Models.DTOs.Booking;

namespace ResturangDB_API.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepo _bookingRepo;
        private readonly ITableRepo _tableRepo;

        public BookingService(IBookingRepo bookingRepo, ITableRepo tableRepo)
        {
            _bookingRepo = bookingRepo;
            _tableRepo = tableRepo;
        }

        public async Task AddBookingAsync(BookingCreateDTO booking)
        {
            var newBooking = new Booking
            {
                FK_TableID = booking.TableID,
                AmountOfPeople = booking.AmountOfPeople,
                Time = booking.Time,
                TimeEnd = booking.TimeEnd,
                Name = booking.Name,
                Email = booking.Email,
                PhoneNumber = booking.PhoneNumber
            };

            await _bookingRepo.AddBookingAsync(newBooking);
        }

        public async Task<IEnumerable<BookingGetDTO>> GetAllBookingsAsync()
        {
            var bookings = await _bookingRepo.GetAllBookingsAsync();

            var bookingList = bookings.Select(booking => new BookingGetDTO
            {
                BookingID = booking.BookingID,
                TableID = booking.FK_TableID,
                AmountOfPeople = booking.AmountOfPeople,
                Time = booking.Time,
                TimeEnd = booking.TimeEnd,
                Name = booking.Name,
                Email = booking.Email,
                PhoneNumber = booking.PhoneNumber
            }).ToList();

            return bookingList;
        }

        public async Task<BookingGetDTO> GetBookingByIdAsync(int bookingID)
        {
            var bookingFound = await _bookingRepo.GetBookingByIDAsync(bookingID);

            if (bookingFound != null)
            {
                var booking = new BookingGetDTO
                {
                    BookingID = bookingFound.BookingID,
                    TableID = bookingFound.FK_TableID,
                    AmountOfPeople = bookingFound.AmountOfPeople,
                    Time = bookingFound.Time,
                    TimeEnd = bookingFound.TimeEnd,
                    Name = bookingFound.Name,
                    Email = bookingFound.Email,
                    PhoneNumber = bookingFound.PhoneNumber
                };

                return booking;
            }

            return null;
        }

        public async Task<bool> UpdateBookingAsync(BookingUpdateDTO booking)
        {
            var bookingFound = await _bookingRepo.GetBookingByIDAsync(booking.BookingID);

            if (bookingFound != null)
            {
                bookingFound.FK_TableID = booking.TableID;
                bookingFound.Time = booking.Time;
                bookingFound.TimeEnd = booking.TimeEnd;
                bookingFound.AmountOfPeople = booking.AmountOfPeople;
                bookingFound.Name = booking.Name;
                bookingFound.Email = booking.Email;
                bookingFound.PhoneNumber = booking.PhoneNumber;
                await _bookingRepo.UpdateBookingAsync(bookingFound);

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteBookingAsync(int bookingID)
        {
            var bookingFound = await _bookingRepo.GetBookingByIDAsync(bookingID);
            
            if (bookingFound != null)
            {
                await _bookingRepo.DeleteBookingAsync(bookingFound);
                return true;
            }

            return false;
        }
    }
}
