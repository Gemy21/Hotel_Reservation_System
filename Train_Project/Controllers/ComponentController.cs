using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Train_Project.DTOs.Components;
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
    public class ComponentController : ControllerBase
    {
        private readonly IComponentService _componentService;

        public ComponentController(IComponentService componentService)
        {
            _componentService = componentService;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _componentService.GetAllComponents());
        }

        [HttpGet]
        [Route("getbyid/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var component = await _componentService.GetComponentById(id);
            if (component == null)
            {
                return NotFound();
            }
            return Ok(component);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreateComponentsDto componentDto)
        {
            await _componentService.CreateComponent(componentDto);
            return Ok();

        }
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _componentService.DeleteComponent(id);
            return Ok();
        }
    }
}
