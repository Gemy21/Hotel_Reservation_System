using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Train_Project.DTOs.Hotel;
using Train_Project.Services.Interfaces;

namespace Train_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateHotelDto dto)
        {
            var result = await _hotelService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _hotelService.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _hotelService.GetByIdAsync(id);

            if (result == null)
                return NotFound("Hotel not found");

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            UpdateHotelDto dto)
        {
            var result = await _hotelService.UpdateAsync(id, dto);

            if (result == null)
                return NotFound("Hotel not found");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var hotel = await _hotelService.GetByIdAsync(id);

            if (hotel == null)
                return NotFound("Hotel not found");

            var result = await _hotelService.DeleteAsync(id);

            if (!result)
                return BadRequest(
                    "Cannot delete hotel because it has related data");

            return Ok("Hotel deleted successfully");
        }
    }
}
