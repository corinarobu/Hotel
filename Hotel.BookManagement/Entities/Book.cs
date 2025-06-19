using Hotel.AccountManagement.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BookManagement.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoomId {  get; set; }
        [Column(TypeName ="decimal(18,2)")]
        public double PricePerNight {  get; set; }
        public BookingStatus Status { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string? StripePaymentIntentId { get; set; }

    }
    public enum BookingStatus
    {
        Pending,
        Confirmed,
        CheckedIn,
        CheckedOut,
        Cancelled
    }
    public enum PaymentStatus
    {
        Pending,
        Paid,
        Failed,
        Refunded
    }

}
