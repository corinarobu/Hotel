using Hotel.AccountManagement.Entities;
using Hotel.AccountManagement.Data;
using Hotel.AccountManagement.DTOs; 
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Hotel.AccountManagement.Interfaces;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using AutoMapper;
using System.Security.Claims;
using Google.Apis.Auth;
namespace Hotel.Server.Controllers.AccountManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountManagementController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountManagementController(UserManager<User> userManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var user = _mapper.Map<User>(registerDto);
            user.UserName = NormalizeUserName(registerDto.FullName);
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);
            if (user.Email == null) return Unauthorized("Invalid email");
            return Ok(new UserDto
            {
                Username = user.UserName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user)
            });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                return Unauthorized("Invalid login attempt.");
            }

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!result)
            {
                return Unauthorized("Invalid login attempt.");
            }
            if (user.Email == null) return Unauthorized("Invalid Email");
            if (user.UserName == null) return Unauthorized("Invalid Username");
            return Ok(new UserDto
            {
                Username = user.UserName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user)
            });
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            return Ok(new { message = "Logged out successfully" });
        }
        private string NormalizeUserName(string fullName)
        {
            return fullName.Trim().Replace(" ", "_").ToLower();
        }
        [HttpGet("current-user-id")]
        public async Task<IActionResult> GetUserDetails()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Console.WriteLine($"User ID: {userId}");
            var user = await _userManager.GetUserAsync(User);
            return Ok(userId);
        }

    }
}

