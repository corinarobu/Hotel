using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.HotelManagement.DTOs
{
    public class RatingDTO
    {
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public int Score { get; set; } 
    }
}
