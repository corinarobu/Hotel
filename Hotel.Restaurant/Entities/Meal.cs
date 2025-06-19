using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Restaurant.Entities
{
    public class Meal
    {
        public int Meal_Id {  get; set; }
        public string Meal_Name { get; set; }= string.Empty;
        public string Meal_Description { get; set; }= string.Empty;
        public List<Products> Products { get; set; } = new List<Products>();
    }
}
