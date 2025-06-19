using Hotel.AccountManagement.Data;
using Hotel.AccountManagement.Interfaces;
using Hotel.BookManagement.Data;
using Hotel.BookManagement.DTOs;
using Hotel.BookManagement.Entities;
using Hotel.BookManagement.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.BookManagement.Services
{
    public class BookService : IBookService
    {
        private readonly BookDbContext _context;
        private readonly AccountManagementDbContext _accountContext;
        private readonly IPaymentService _paymentService;

        public BookService(BookDbContext context, AccountManagementDbContext accountContext, IPaymentService paymentService)
        {
            _context = context;
            _accountContext = accountContext;
            _paymentService = paymentService;
        }
        public async Task<BookGetDTO> AddBookingAsync(BookAddDTO dto, int userId)
        {
            _ = await _accountContext.Users.FindAsync(userId) ?? throw new Exception("No user found!");

            var availabilityRequest = new BookAvailableDTO
            {
                RoomId = dto.RoomId,
                CheckInDate = dto.CheckInDate,
                CheckOutDate = dto.CheckOutDate
            };

            if (!await CheckRoomAvailability(availabilityRequest))
                throw new Exception("Room is already booked for this period");

            var newBooking = new Book
            {
                UserId = userId,
                RoomId = dto.RoomId,
                PricePerNight = dto.PricePerNight,
                CheckInDate = dto.CheckInDate,
                CheckOutDate = dto.CheckOutDate,
                Status = BookingStatus.Pending,
                PaymentStatus = PaymentStatus.Pending
            };

            _context.Books.Add(newBooking);
            await _context.SaveChangesAsync();
            var addedBooking = await _accountContext.AccountsUsers.FirstOrDefaultAsync(bo => bo.UserId == userId) ?? throw new Exception("Failed to add booking");
            return new BookGetDTO
            {
                Id = newBooking.Id,
                UserId = newBooking.UserId,
                RoomId = newBooking.RoomId,
                PricePerNight = newBooking.PricePerNight,
                CheckInDate = newBooking.CheckInDate,
                CheckOutDate = newBooking.CheckOutDate,
                Status = newBooking.Status,
                PaymentStatus = newBooking.PaymentStatus
            };

        }

        public async Task<bool> CheckRoomAvailability(BookAvailableDTO request)
        {
            return !await _context.Books.AnyAsync(b =>
                b.RoomId == request.RoomId &&
                b.Status != BookingStatus.Cancelled &&
                ((request.CheckInDate >= b.CheckInDate && request.CheckOutDate <= b.CheckOutDate) ||
                (request.CheckOutDate >= b.CheckInDate && request.CheckOutDate <= b.CheckOutDate) ||
                (request.CheckInDate <= b.CheckInDate && request.CheckOutDate >= b.CheckOutDate)));
        }

        public async Task UpdatePaymentStatusAsync(int bookingId, PaymentStatus status)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var booking = await _context.Books.FirstOrDefaultAsync(b => b.Id == bookingId);
                if (booking == null)
                    throw new Exception("Booking not found");

                booking.PaymentStatus = status;
                if (status == PaymentStatus.Paid)
                    booking.Status = BookingStatus.Confirmed;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task CancelBookingAsync(int bookingId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var booking = await _context.Books.FirstOrDefaultAsync(b => b.Id == bookingId);
                if (booking == null)
                    throw new Exception("Booking not found");

                if (booking.Status == BookingStatus.Cancelled)
                    throw new Exception("Booking is already cancelled");

                booking.Status = BookingStatus.Cancelled;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> HasUserBookedRoom(int userId, int roomId)
        {
            return await _context.Books
                .AnyAsync(b => b.UserId == userId && b.RoomId == roomId);
        }
        public async Task<bool> HasUserBookedRoomInAPeriod(int roomId, DateOnly checkInDate, DateOnly checkOutDate)
        {
            var existingBooking = await _context.Books
                .Where(b => b.RoomId == roomId &&
                            ((b.CheckInDate >= checkInDate && b.CheckInDate <= checkOutDate) ||
                             (b.CheckOutDate >= checkInDate && b.CheckOutDate <= checkOutDate)))
                .FirstOrDefaultAsync();

            return existingBooking != null;
        }

        public async Task<List<BookGetDTO>> GetBookingsByUserAsync(int userId)
        {
            var bookings = await _context.Books
                .Where(b => b.UserId == userId)
                .ToListAsync();

            return bookings.Select(b => new BookGetDTO
            {
                Id = b.Id,
                UserId = b.UserId,
                RoomId = b.RoomId,
                PricePerNight = b.PricePerNight,
                CheckInDate = b.CheckInDate,
                CheckOutDate = b.CheckOutDate,
                Status = b.Status,
                PaymentStatus = b.PaymentStatus
            }).ToList();
        }

    }
}
