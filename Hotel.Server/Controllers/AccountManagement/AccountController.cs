using Hotel.AccountManagement.DTOs;
using Hotel.AccountManagement.Interfaces;
using Hotel.HotelManagement.DTOs;
using Hotel.HotelManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Server.Controllers.AccountManagement
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserGetDTO>>> SearchUsers([FromQuery] string? searchTerm = null)
        {
            var users = await _accountService.SearchUsersAsync(searchTerm);
            return Ok(users);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _accountService.DeleteUserAsync(id);
            if (!result)
            {
                return NotFound("User not found.");
            }

            return NoContent();
        }

    }
}
