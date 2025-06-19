using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.HotelManagement.Entities
{
    public class Room
    {
        public enum PlanMeal
        {
            allinclusive = 1,
            none = 0,
        }
        public enum View
        {
            street = 0,
            mountain = 1,
        }
        public int Id_Room { get; set; }
        public int RoomNumber { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }
        public View ViewType { get; set; }
        public bool HasBreakfastIncluded { get; set; }
        public PlanMeal MealPlan { get; set; }
        public DateOnly startDate { get; set; }
        public DateOnly endDate { get; set; }
        public ICollection<FAQ> FAQs { get; set; } = new List<FAQ>();

    }

}
