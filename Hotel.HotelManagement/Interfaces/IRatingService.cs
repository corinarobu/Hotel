using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.HotelManagement.Interfaces
{
    public interface IRatingService
    {
        Task<bool> SubmitRating(int userId, int roomId, int rating);
        Task<double?> GetAverageRating(int roomId);
    }
}
