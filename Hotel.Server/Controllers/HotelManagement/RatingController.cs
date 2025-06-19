using Hotel.AccountManagement.Entities;
using Hotel.HotelManagement.DTOs;
using Hotel.HotelManagement.Interfaces;
using Hotel.HotelManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class RatingController : ControllerBase
{
    private readonly IRatingService _ratingService;
    private readonly UserManager<User> _userManager;

    public RatingController(IRatingService ratingService, UserManager<User>userManager)
    {
        _ratingService = ratingService;
        _userManager = userManager;
    }

    [Authorize]
    [HttpPost("SubmitRating")]
    public async Task<IActionResult> SubmitRating([FromBody] RatingDTO ratingDto)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();
        bool success = await _ratingService.SubmitRating(user.Id, ratingDto.RoomId, ratingDto.Score);

        if (!success)
        {
            return BadRequest("You cannot rate a room you haven't booked or you rate it once!");
        }

        return Ok(new { text = "Rating submitted successfully." });

    }
    [HttpGet("average/{roomId}")]
    public async Task<IActionResult> GetAverageRating(int roomId)
    {
        var averageRating = await _ratingService.GetAverageRating(roomId);

        if (averageRating == null)
        {
            return NotFound("No ratings available for this room.");
        }

        return Ok(new { RoomId = roomId, AverageRating = averageRating });
    }
}
