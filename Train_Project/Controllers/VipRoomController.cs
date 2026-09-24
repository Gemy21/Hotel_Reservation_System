using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Train_Project.Data;
using Train_Project.DTOs.Rooms;
using Train_Project.Services.Interfaces;

namespace Train_Project.Controllers
{
    [Route("api/vip-rooms")]
    [ApiController]
    public class VipRoomController : ControllerBase
    {
        private readonly IVipRoomService _vipRoomService;
        public VipRoomController(IVipRoomService vipRoomService) => _vipRoomService = vipRoomService;

        [HttpGet]
        public async Task<IActionResult> GetAllVipRooms() =>
            Ok(await _vipRoomService.GetAllVipRoomsAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVipRoomById(int id)
        {
            var vipRoom = await _vipRoomService.GetVipRoomByIdAsync(id);
            return vipRoom is null ? NotFound($"VIP room with id {id} was not found.") : Ok(vipRoom);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVipRoom([FromBody] CreateVipRoomDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var vipRoom = await _vipRoomService.CreateVipRoomAsync(dto);
                return CreatedAtAction(nameof(GetVipRoomById), new { id = vipRoom.Id }, vipRoom);
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
            catch (InvalidOperationException ex) { return Conflict(ex.Message); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVipRoom(int id, [FromBody] UpdateVipRoomDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var updated = await _vipRoomService.UpdateVipRoomAsync(id, dto);
                return updated is null ? NotFound($"VIP room with id {id} was not found.") : Ok(updated);
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVipRoom(int id)
        {
            var deleted = await _vipRoomService.DeleteVipRoomAsync(id);
            return deleted ? NoContent() : NotFound($"VIP room with id {id} was not found.");
        }

        [HttpGet("{id}/services")]
        public async Task<IActionResult> GetVipRoomServices(int id)
        {
            var services = await _vipRoomService.GetVipRoomServicesAsync(id);
            return services is null ? NotFound($"VIP room with id {id} was not found.") : Ok(services);
        }

        [HttpPost("{id}/late-checkout")]
        public async Task<IActionResult> RequestLateCheckOut(int id, [FromBody] LateCheckOutRequestDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var result = await _vipRoomService.RequestLateCheckOutAsync(id, dto);
                return result is null ? NotFound($"VIP room with id {id} was not found.") : Ok(result);
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
            catch (InvalidOperationException ex) { return Conflict(ex.Message); }
        }
    }
}
