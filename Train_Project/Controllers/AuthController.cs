using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Train_Project.Authentication;
using Train_Project.Authentication.AuthEntity;
using Train_Project.Data;

namespace Train_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IAuthService authService, AppDbContext context) : ControllerBase
    {

        [HttpPost]
        [Route("authenticate")]
        public ActionResult<string> Post(AuthenticationRequest request)
        {
            var user = context.Set<Users>()
                .FirstOrDefault(x => x.Username == request.Username && x.Password == request.Password);

            var token = authService.GenerateToken(user.Id, user.Username, user.Roles);
            return Ok(token);
        }
    }
}
