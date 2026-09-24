using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Train_Project.DTOs.Rooms;
using Train_Project.Filters;
using Train_Project.Services.Interfaces;

namespace Train_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(RequestTimingFilter))]
    [ServiceFilter(typeof(ModelValidationFilter))]
    [ServiceFilter(typeof(DateRangeFilter))]
    [Authorize]
    public class StandardRoomController : ControllerBase
    {
        private readonly IStandardRoomServices _standardRoomService;

        public StandardRoomController(IStandardRoomServices standardRoomService)
        {
            _standardRoomService = standardRoomService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(CreateRoomDto dto)
        {
            var result = await _standardRoomService.CreateRoomAsync(dto);

            if (!result)
                return BadRequest("Invalid room data or building/room number already exists.");

            return Ok("Room created successfully");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoom(int id)
        {
            var room = await _standardRoomService.GetRoomByIdAsync(id);

            if (room == null)
                return NotFound("Room not found.");

            return Ok(room);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(int id, UpdateRoomDto dto)
        {
            var result = await _standardRoomService.UpdateRoomAsync(id, dto);

            if (!result)
                return BadRequest("Invalid room data or room not found.");

            return Ok("Room updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var result = await _standardRoomService.DeleteRoomAsync(id);

            if (!result)
                return BadRequest("Room not found or has reservations.");

            return Ok("Room deleted successfully");
        }

        [HttpGet("{id}/availability")]
        public async Task<IActionResult> CheckAvailability(
    int id,
    DateOnly from,
    DateOnly to)
        {
            var result = await _standardRoomService
                .CheckRoomAvailabilityAsync(id, from, to);

            return Ok(new
            {
                roomId = id,
                from,
                to,
                isAvailable = result
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRooms()
        {
            var rooms = await _standardRoomService.GetAllRoomsAsync();

            return Ok(rooms);
        }
    }
}
