using Hotel.BookManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BookManagement.DTOs
{
    public class BookGetDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public double PricePerNight { get; set; }
        public BookingStatus Status { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
     
    }
}
