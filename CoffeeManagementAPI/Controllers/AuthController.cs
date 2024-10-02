using Microsoft.AspNetCore.Mvc;

namespace CoffeeManagementAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/auth")]
    public class AuthController : ControllerBase
    {

        [HttpPost("register")]
        public async Task<IActionResult> Register()
        {
            return Ok(ModelState);
        }

        


    }
}
