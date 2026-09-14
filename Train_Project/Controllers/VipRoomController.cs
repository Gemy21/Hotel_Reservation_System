using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Train_Project.Data;
using Train_Project.Services.Interfaces;

namespace Train_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VipRoomController : ControllerBase
    {
        private readonly IVipRoomService vipRoomService;

        public VipRoomController( IVipRoomService vipRoomService)
        {
            this.vipRoomService = vipRoomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVipRooms()
        {
            var vipRooms = await vipRoomService.GetAllVipRoomsAsync();
            return Ok(vipRooms);
        }

    }
}
