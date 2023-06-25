using Microsoft.AspNetCore.Mvc;
using TeamManagement.Data.Etintities;
using TeamManagement.Services.Dtos;
using TeamManagement.Services;

namespace TeamManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController:ControllerBase
    {
         
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register(UserRegisterDto registerData)
        {
            User user = _userService.Register(registerData);

            if (user == null)
            {
                return BadRequest("User cannot be created");
            }

            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult Login(UserLoginDto loginData)
        {
            if (loginData == null)
            {
                return BadRequest("Invalid client request!");
            }

            var token = _userService.Login(loginData);

            return Ok(token);
        }
    }
}
