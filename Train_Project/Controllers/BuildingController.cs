using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Train_Project.DTOs.Buildings;
using Train_Project.Services.Interfaces;

namespace Train_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private readonly IBuildingService _buildingService;

        public BuildingController(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _buildingService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _buildingService.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBuildingDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _buildingService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBuildingDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _buildingService.UpdateAsync(id, dto);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _buildingService.DeleteAsync(id);
                if (!result) return NotFound();

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



    }
}
