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

        private static DateTime GetNextWholeHourUtc(DateTime utcNow)
        {
            var truncated = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day, utcNow.Hour, 0, 0, DateTimeKind.Utc);
            return truncated.AddHours(1);
        }

        public BookingService(IBookingRepo bookingRepo, ITableRepo tableRepo)
        {
            _bookingRepo = bookingRepo;
            _tableRepo = tableRepo;
        }

        private static bool Overlaps(DateTime startA, DateTime endA, DateTime startB, DateTime endB)
            => startA < endB && endA > startB;

        private static void ValidateTimeWindow(DateTime start, DateTime end)
        {
            if (end <= start)
            {
                throw new ArgumentException("End time must be after start time.");
            }

            var nowUtc = DateTime.UtcNow;
            var earliestUtc = GetNextWholeHourUtc(nowUtc);
            var latestUtc = nowUtc.AddMonths(1);

            var startUtc = start.ToUniversalTime();

            if (startUtc < earliestUtc)
            {
                throw new ArgumentException("Booking start time must be at or after the next whole hour from now.");
            }

            if (startUtc > latestUtc)
            {
                throw new ArgumentException("Booking start time must be within 1 month from now.");
            }
        }

        public async Task AddBookingAsync(BookingCreateDTO booking)
        {
            ValidateTimeWindow(booking.Time, booking.TimeEnd);

            var table = await _tableRepo.GetTableByIDAsync(booking.TableID);
            if (table == null)
            {
                throw new InvalidOperationException("The specified table does not exist.");
            }

            if (!table.IsAvailable)
            {
                throw new InvalidOperationException("The specified table is not available.");
            }

            if (booking.AmountOfPeople <= 0)
            {
                throw new ArgumentException("AmountOfPeople must be greater than 0.");
            }

            if (table.TableSeats < booking.AmountOfPeople)
            {
                throw new InvalidOperationException("Too many people for the selected table.");
            }

            var existingBookings = await _bookingRepo.GetAllBookingsAsync();
            var overlapExists = existingBookings.Any(b =>
                b.FK_TableID == booking.TableID &&
                Overlaps(booking.Time, booking.TimeEnd, b.Time, b.TimeEnd));

            if (overlapExists)
            {
                throw new InvalidOperationException("This table is already booked for the selected time window.");
            }

            var newBooking = new Booking
            {
                FK_TableID = booking.TableID,
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
                ValidateTimeWindow(booking.Time, booking.TimeEnd);

                var table = await _tableRepo.GetTableByIDAsync(booking.TableID);
                if (table == null)
                {
                    throw new InvalidOperationException("The specified table does not exist.");
                }

                if (!table.IsAvailable)
                {
                    throw new InvalidOperationException("The specified table is not available.");
                }

                if (booking.AmountOfPeople <= 0)
                {
                    throw new ArgumentException("AmountOfPeople must be greater than 0.");
                }

                if (table.TableSeats < booking.AmountOfPeople)
                {
                    throw new InvalidOperationException("Too many people for the selected table.");
                }

                var existingBookings = await _bookingRepo.GetAllBookingsAsync();
                var overlapExists = existingBookings.Any(b =>
                    b.BookingID != booking.BookingID &&
                    b.FK_TableID == booking.TableID &&
                    Overlaps(booking.Time, booking.TimeEnd, b.Time, b.TimeEnd));

                if (overlapExists)
                {
                    throw new InvalidOperationException("This table is already booked for the selected time window.");
                }

                bookingFound.FK_TableID = booking.TableID;
                bookingFound.Time = booking.Time;
                bookingFound.TimeEnd = booking.TimeEnd;
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
