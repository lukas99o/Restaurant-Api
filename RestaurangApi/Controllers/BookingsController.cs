using Microsoft.AspNetCore.Mvc;
using ResturangDB_API.Data.Repos.IRepos;
using ResturangDB_API.Models.DTOs.Booking;
using ResturangDB_API.Services.IServices;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace ResturangDB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        [Route("CreateBooking")]
        public async Task<IActionResult> CreateBooking(BookingCreateDTO booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _bookingService.AddBookingAsync(booking);
            return Created();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingGetDTO>>> GetAllBookings()
        {
            var bookingList = await _bookingService.GetAllBookingsAsync();
            return Ok(bookingList);
        }

        [HttpGet]
        [Route("GetSpecificBooking/{bookingID}")]
        public async Task<ActionResult<BookingGetDTO>> GetSpecificBooking(int bookingID)
        {
            var booking = await _bookingService.GetBookingByIdAsync(bookingID);

            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }

        [HttpPut]
        [Route("UpdateBooking/{bookingID}")]
        public async Task<IActionResult> UpdateSpecificBooking(int bookingID, BookingUpdateDTO booking)
        {
            if (bookingID != booking.BookingID)
            {
                return BadRequest();
            }

            var result = await _bookingService.UpdateBookingAsync(booking);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteBooking/{bookingID}")]
        public async Task<IActionResult> DeleteSpecificBooking(int bookingID)
        {
            var result = await _bookingService.DeleteBookingAsync(bookingID);
            
            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
