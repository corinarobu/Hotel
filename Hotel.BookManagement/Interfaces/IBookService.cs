using Hotel.BookManagement.DTOs;
using Hotel.BookManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BookManagement.Interfaces
{
    public interface IBookService
    {
        Task<BookGetDTO> AddBookingAsync(BookAddDTO dto, int userId);
        Task UpdatePaymentStatusAsync(int bookingId, PaymentStatus status);
        Task<bool> CheckRoomAvailability(BookAvailableDTO request);
        Task<bool> HasUserBookedRoom(int userId, int roomId);
        Task<bool> HasUserBookedRoomInAPeriod(int roomId, DateOnly checkInDate, DateOnly checkOutDate);
        Task<List<BookGetDTO>> GetBookingsByUserAsync(int userId);
        Task CancelBookingAsync(int bookingId);
    }
}