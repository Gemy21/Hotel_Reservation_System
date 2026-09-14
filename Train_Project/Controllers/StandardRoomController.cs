using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Train_Project.Services.Interfaces;

namespace Train_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StandardRoomController : ControllerBase
    {
        private readonly IStandardRoomServices _standardRoomService;

        public StandardRoomController(IStandardRoomServices standardRoomService)
        {
            _standardRoomService = standardRoomService;
        }
    }
}
