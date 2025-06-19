using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Restaurant.DTOs
{
    public class MealGetDTO
    {
        public int Meal_Id { get; set; }
        public string Meal_Name { get; set; } = string.Empty;
        public string Meal_Description { get; set; } = string.Empty;
    }
}
