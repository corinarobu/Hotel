using Hotel.HotelManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Hotel.HotelManagement.Entities.Room;

namespace Hotel.HotelManagement.DTOs
{
    public class RoomGetDTO
    {
        public int Id_Room { get; set; }
        public int RoomNumber { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }
        public View ViewType { get; set; }
        public bool HasBreakfastIncluded { get; set; }
        public PlanMeal MealPlan { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public ICollection<FAQ> FAQs { get; set; } = new List<FAQ>();

    }
}
