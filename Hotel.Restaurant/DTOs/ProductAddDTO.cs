using Hotel.Restaurant.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Restaurant.DTOs
{
    public class ProductAddDTO
    {
        public string Name_Product { get; set; } = string.Empty;
        public string Type_Of_Product { get; set; } = string.Empty;
        public string Description_Product { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Meal_Id { get; set; }
    }
}
