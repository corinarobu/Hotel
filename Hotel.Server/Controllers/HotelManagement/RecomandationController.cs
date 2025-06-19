using Hotel.AccountManagement.Entities;
using Hotel.HotelManagement.DTOs;
using Hotel.HotelManagement.Interfaces;
using Hotel.HotelManagement.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Hotel.Server.Controllers.HotelManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecomandationController : ControllerBase
    {
        private readonly IRecomandationService _recomandationService;
        private readonly UserManager<User> _userManager;

        public RecomandationController(IRecomandationService recomandationService, UserManager<User> userManager)
        {
            _recomandationService = recomandationService;
            _userManager = userManager;

        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddRecomandation(RecomandationAddDTO recomandationAddDTO)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();
            recomandationAddDTO.setId(user.Id);
            var result = await _recomandationService.AddRecomandationAsync(recomandationAddDTO,user.Id);
            return CreatedAtAction(nameof(GetRecomandationById), new { id = result.Id_Recomandation }, result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecomandationById(int id)
        {
            var result = await _recomandationService.GetRecomandationByIdAsync(id);
            if (result == null) return NotFound();

            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRecomandations()
        {
            var result = await _recomandationService.GetAllRecomandationsAsync();
            if (result == null) return NotFound();

            return Ok(result);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecomandation(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var result = await _recomandationService.DeleteRecomandationAsync(id, user.Id);
    
            if (!result) return NotFound(); 
    
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecomandation(int id, [FromBody] RecomandationAddDTO recomandationAddDTO)
        {
            if (recomandationAddDTO == null)
            {
                return BadRequest("Recomandation data is required.");
            }

            var updatedRecomandation = await _recomandationService.UpdateRecomandationAsync(id, recomandationAddDTO);
            if (updatedRecomandation == null)
            {
                return NotFound($"Recomandation with ID {id} was not found.");
            }

            return Ok(updatedRecomandation);
        }
    }
}
