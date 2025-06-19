using Hotel.AccountManagement.Entities;
using Hotel.HotelManagement.DTOs;
using Hotel.HotelManagement.Interfaces;
using Hotel.HotelManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Server.Controllers.HotelManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class FAQController : ControllerBase
    {
       private readonly IFAQService _faqService;
        private readonly UserManager<User> _userManager;

        public FAQController(IFAQService faqService, UserManager<User> userManager)
        {
            _faqService = faqService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddFAQ(FAQAddDTO faqAddDTO)
        {
            var result = await _faqService.AddFAQAsync(faqAddDTO);
            return CreatedAtAction(nameof(GetFAQById), new { id = result.Id_FAQ }, result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFAQById(int id)
        {
            var result = await _faqService.GetFAQByIdAsync(id);
            if (result == null) return NotFound();

            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFAQ(int id)
        {
            var result = await _faqService.DeleteFAQAsync(id);
            if (result == null) return NotFound();
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFAQ(int id, [FromBody] FAQAddDTO faqAddDTO)
        {
            if (faqAddDTO == null)
            {
                return BadRequest("FAQ data is required.");
            }

            var updatedFAQ = await _faqService.UpdateFAQAsync(id, faqAddDTO);
            if (updatedFAQ == null)
            {
                return NotFound($"FAQ with ID {id} was not found.");
            }

            return Ok(updatedFAQ);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRecomandations()
        {
            var result = await _faqService.GetAllFAQs();
            if (result == null) return NotFound();

            return Ok(result);
        }
        
    }
}
