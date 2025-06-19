using Hotel.HotelManagement.DTOs;
using Hotel.HotelManagement.Interfaces;
using Hotel.HotelManagement.Services;
using Hotel.Restaurant.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Server.Controllers.HotelManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService roomService;

        public RoomController(IRoomService roomService)
        {
            this.roomService = roomService;
        }
        [HttpPost]
        public async Task<IActionResult> AddRoom(RoomAddDTO roomAddDTO)
        {
            var result = await roomService.AddRoomAsync(roomAddDTO);
            return CreatedAtAction(nameof(GetRoomById), new { id = result.Id_Room }, result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomById(int id)
        {
            var result = await roomService.GetRoomByIdAsync(id);
            if (result == null) return NotFound();

            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var result = await roomService.DeleteRoomAsync(id);
            if (result == null) return NotFound();
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(int id, [FromBody] RoomAddDTO roomAddDTO)
        {
            if (roomAddDTO == null)
            {
                return BadRequest("Room data is required.");
            }

            var updatedRoom = await roomService.UpdateRoomAsync(id, roomAddDTO);
            if (updatedRoom == null)
            {
                return NotFound($"Room with ID {id} was not found.");
            }

            return Ok(updatedRoom);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomGetDTO>>> GetAllRooms()
        {
            var room = await roomService.GetAllRoomsAsync();
            return Ok(room);
        }
        [HttpGet("filtered")]
        public async Task<ActionResult<IEnumerable<RoomGetDTO>>> GetFilteredRooms(
              [FromQuery] int? capacity,
              [FromQuery] decimal? minPrice,
              [FromQuery] decimal? maxPrice,
              [FromQuery] bool? isAvailable,
              [FromQuery] int? viewType,
              [FromQuery] bool? hasBreakfastIncluded,
              [FromQuery] int? mealPlan)
        {
            var rooms = await roomService.FilteredRoomsAsync(capacity, minPrice, maxPrice, isAvailable, viewType, hasBreakfastIncluded, mealPlan);
            return Ok(rooms);
        }

    }
}
