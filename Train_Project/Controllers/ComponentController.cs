using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Train_Project.Services.Interfaces;

namespace Train_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentController : ControllerBase
    {
        private readonly IComponentService _componentService;

        public ComponentController(IComponentService componentService)
        {
            _componentService = componentService;
        }
    }
}
