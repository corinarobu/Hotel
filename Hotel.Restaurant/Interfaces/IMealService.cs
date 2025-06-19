using Hotel.Restaurant.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Restaurant.Interfaces
{
    public interface IMealService
    {
        Task<MealGetDTO> AddMealAsync(MealAddDTO mealDto);
        Task<MealGetDTO> UpdateMealAsync(int id_Meal, MealAddDTO mealDto);
        Task<bool> DeleteMealAsync(int id_Meal);
        Task<MealGetDTO> GetMealByIdAsync(int id_Meal);
        Task<IEnumerable<MealGetDTO>> GetAllMealsAsync();
    }
}
