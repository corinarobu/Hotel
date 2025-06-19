using Hotel.Restaurant.Data;
using Hotel.Restaurant.DTOs;
using Hotel.Restaurant.Entities;
using Hotel.Restaurant.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Restaurant.Services
{
    public class MealService : IMealService
    {
        private readonly RestaurantDbContext _context;

        public MealService(RestaurantDbContext context)
        {
            _context = context;
        }
        public async Task<MealGetDTO> AddMealAsync(MealAddDTO mealDto)
        {
            var meal = new Meal { Meal_Name = mealDto.Meal_Name, Meal_Description = mealDto.Meal_Description };
            _context.Meals.Add(meal);
            await _context.SaveChangesAsync();

            return new MealGetDTO { Meal_Id= meal.Meal_Id, Meal_Name = meal.Meal_Name, Meal_Description=meal.Meal_Description };
        }

        public async Task<bool> DeleteMealAsync(int id_Meal)
        {
            var meal = await _context.Meals.FindAsync(id_Meal);
            if (meal == null) { return false; }
            _context.Meals.Remove(meal);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<MealGetDTO> UpdateMealAsync(int id_Meal, MealAddDTO mealDto)
        {
            var meal = await _context.Meals.FindAsync(id_Meal);
            if (meal == null)
            {
                return null;
            }

            meal.Meal_Name = mealDto.Meal_Name;
            meal.Meal_Description = mealDto.Meal_Description;

            await _context.SaveChangesAsync();

            return new MealGetDTO
            {
                Meal_Id = meal.Meal_Id,
                Meal_Name = meal.Meal_Name,
                Meal_Description = meal.Meal_Description
              
            };
        }
        public async Task<MealGetDTO> GetMealByIdAsync(int id)
        {
            var meal = await _context.Meals.FindAsync(id);
            if (meal == null) return null;

            return new MealGetDTO
            {
                Meal_Id = meal.Meal_Id,
                Meal_Name = meal.Meal_Name,
                Meal_Description = meal.Meal_Description
            };
        }
        public async Task<IEnumerable<MealGetDTO>> GetAllMealsAsync()
        {
            var meals = await _context.Meals.ToListAsync();

            return meals.Select(meal => new MealGetDTO
            {
                Meal_Id = meal.Meal_Id,
                Meal_Name = meal.Meal_Name,
                Meal_Description = meal.Meal_Description
            });
        }

    }
}
