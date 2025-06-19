
using Hotel.HotelManagement.Interfaces;
using Hotel.Restaurant.DTOs;
using Hotel.Restaurant.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Server.Controllers.Restaurant
{
  
        [Route("api/[controller]")]
        [ApiController]
        public class MealController : ControllerBase
        {
            private readonly IMealService _mealService;

            public MealController(IMealService mealService)
            {
                _mealService = mealService;
            }

            [HttpPost]
            public async Task<IActionResult> AddMeal(MealAddDTO mealAddDTO)
            {
                var result = await _mealService.AddMealAsync(mealAddDTO);
                return CreatedAtAction(nameof(GetMealById), new { id = result.Meal_Id }, result);
            }
            [HttpGet("{id}")]
            public async Task<IActionResult> GetMealById(int id)
            {
                var result = await _mealService.GetMealByIdAsync(id);
                if (result == null) return NotFound();

                return Ok(result);
            }
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteMeal(int id)
            {
                var result = await _mealService.DeleteMealAsync(id);
                if (result == null) return NotFound();
                return NoContent();
            }
            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateMeal(int id, [FromBody] MealAddDTO mealAddDTO)
            {
                if (mealAddDTO == null)
                {
                    return BadRequest("Meal data is required.");
                }

                var updatedMeal = await _mealService.UpdateMealAsync(id, mealAddDTO);
                if (updatedMeal == null)
                {
                    return NotFound($"Meal with ID {id} was not found.");
                }

                return Ok(updatedMeal);
            }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MealGetDTO>>> GetAllMeals()
        {
            var meals = await _mealService.GetAllMealsAsync();
            return Ok(meals);
        }
    }
    }
