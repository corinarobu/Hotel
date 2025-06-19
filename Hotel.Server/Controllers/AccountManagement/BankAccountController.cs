using Hotel.AccountManagement.DTOs;
using Hotel.AccountManagement.Entities;
using Hotel.AccountManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hotel.Server.Controllers.AccountManagement
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly IBankAccountService bankAccountService;
        private readonly UserManager<User> _userManager;

        public BankAccountController(IBankAccountService bankAccountService, UserManager<User> userManager)
        {
            this.bankAccountService = bankAccountService;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<ActionResult<List<BankAccountAddDTO>>> GetBankAccounts()
        {
            var user = await _userManager.GetUserAsync(User);
            if(user == null)  return Unauthorized();
            var account = await bankAccountService.GetBankAccountForUser(user.Id);
            return Ok(account);
        }
        [HttpPost]
        public async Task<ActionResult> AddBankAccount([FromBody] BankAccountAddDTO bankAccountAddDTO)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Console.WriteLine($"User ID: {userId}");
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();
            if (bankAccountAddDTO.BankName.Length < 10) return BadRequest("Bank must be grater than 10 characters");
            if (bankAccountAddDTO.IBAN.Length < 12) return BadRequest("IBAN must be grater than 12 chracters");
            try
            {
                await bankAccountService.AddBankAccountAsync(user.Id, bankAccountAddDTO);
                return Ok("Bank Account was added.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request.", details = ex.Message });
            }
        }
    }
}
