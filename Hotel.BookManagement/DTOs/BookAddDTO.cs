using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BookManagement.DTOs
{
    public class BookAddDTO
    {
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public double PricePerNight { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public void setId(int id) => UserId = id;
    }
}
