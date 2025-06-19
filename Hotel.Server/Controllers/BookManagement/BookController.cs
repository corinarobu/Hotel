using Hotel.AccountManagement.Entities;
using Hotel.AccountManagement.Interfaces;
using Hotel.BookManagement.DTOs;
using Hotel.BookManagement.Entities;
using Hotel.BookManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hotel.BookManagement.Controllers
{
    [Authorize]
    [Route("api/bookings")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly UserManager<User> _userManager;


        public BookController(IBookService bookService, UserManager<User> userManager)
        {
            _bookService = bookService;
            _userManager = userManager;

        }
        [HttpPost("add-booking")]
        public async Task<IActionResult> AddBookingAsync([FromBody] BookAddDTO dto)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();
            dto.setId(user.Id);
            var booking = await _bookService.AddBookingAsync(dto, user.Id);
            return Ok(booking);
        }

        [HttpPost("check-availability")]
        public async Task<IActionResult> CheckAvailability([FromBody] BookAvailableDTO request)
        {
            bool isAvailable = await _bookService.CheckRoomAvailability(request);
            return Ok(new { isAvailable });
        }
        [HttpGet("HasUserBookedRoom")]
        public async Task<IActionResult> HasUserBookedRoom(int roomId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();
            bool hasBooked = await _bookService.HasUserBookedRoom(user.Id, roomId);
            return Ok(hasBooked);
        }
        [HttpGet("HasUserBookedRoomInAPeriod")]
        public async Task<IActionResult> HasUserBookedRoom(int roomId, string checkInDate, string checkOutDate)
        {

            Console.WriteLine($"Received Check-in Date: {checkInDate}");
            Console.WriteLine($"Received Check-out Date: {checkOutDate}");

            if (DateTime.TryParse(checkInDate, out var parsedCheckInDate) && DateTime.TryParse(checkOutDate, out var parsedCheckOutDate))
            {
                var checkIn = DateOnly.FromDateTime(parsedCheckInDate);
                var checkOut = DateOnly.FromDateTime(parsedCheckOutDate);

                bool hasBooked = await _bookService.HasUserBookedRoomInAPeriod(roomId, checkIn, checkOut);
                return Ok(hasBooked);
            }

            return BadRequest("Invalid date format.");
        }


        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<BookGetDTO>>> GetBookingsByUser(int userId)
        {
            var bookings = await _bookService.GetBookingsByUserAsync(userId);
            return Ok(bookings);
        }
        [HttpPut("update-payment-status/{bookingId}")]
        public async Task<IActionResult> UpdatePaymentStatus(int bookingId, [FromBody] PaymentStatus status)
        {
            try
            {
                await _bookService.UpdatePaymentStatusAsync(bookingId, status);
                return Ok(new { message = "Payment status updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{bookingId}/cancel")]
        public async Task<IActionResult> CancelBooking(int bookingId)
        {
            await _bookService.CancelBookingAsync(bookingId);
            return NoContent();
        }
    }
}
