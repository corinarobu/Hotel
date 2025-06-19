using Hotel.BookManagement.Interfaces;
using Hotel.HotelManagement.Data;
using Hotel.HotelManagement.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.HotelManagement.Services
{
    public class RatingService : IRatingService
    {

        private readonly HotelManagementDbContext _context;
        private readonly IBookService _bookService;

        public RatingService(HotelManagementDbContext context, IBookService bookService)
        {
            _context = context;
            _bookService = bookService;
        }
        public async Task<bool> SubmitRating(int userId, int roomId, int rating)
        {
            bool hasBooked = await _bookService.HasUserBookedRoom(userId, roomId);

            if (!hasBooked)
            {
                return false;
            }

            var existingRating = await _context.FaqRatings
                                               .FirstOrDefaultAsync(r => r.UserId == userId && r.RoomId == roomId);

            if (existingRating != null)
            {
                return false; 
            }

            var newRating = new Rating
            {
                UserId = userId,
                RoomId = roomId,
                Score = rating,
                CreatedAt = DateTime.UtcNow
            };

            _context.FaqRatings.Add(newRating);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<double?> GetAverageRating(int roomId)
        {
            var ratings = _context.FaqRatings
                                  .Where(r => r.RoomId == roomId);

            if (!ratings.Any())
            {
                return null;
            }

            return await ratings.AverageAsync(r => r.Score);
        }

    }
}
