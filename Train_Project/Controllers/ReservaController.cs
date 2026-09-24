using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Train_Project.DTOs.Reservations;
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
    public class ReservaController : ControllerBase
    {
        private readonly IReservationService _service;

        public ReservaController(IReservationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailable() => Ok(await _service.GetAvailableAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReservationsDto dto)
        {
            try
            {
                var created = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateReservationsDto dto)
        {
            try
            {
                var updated = await _service.UpdateAsync(id, dto);
                return updated == null ? NotFound() : Ok(updated);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("{id:int}/cancel")]
        public async Task<IActionResult> Cancel(int id)
        {
            var ok = await _service.CancelAsync(id);
            return ok ? NoContent() : NotFound();
        }
    }
}