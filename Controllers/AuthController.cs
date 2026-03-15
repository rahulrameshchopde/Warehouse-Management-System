using WarehouseProject.Services.Identity_Access_Management;
using Microsoft.AspNetCore.Mvc;
using WarehouseProject.DTOs;

namespace WarehouseProject.Controllers
{
    namespace WarehousePro.Controllers

    {
        [ApiController]

        [Route("api/[controller]")]

        public class AuthController : ControllerBase

        {

            private readonly IAuthService _authService;

            public AuthController(IAuthService authService)

            {

                _authService = authService;

            }

            // REGISTER USER

            [HttpPost("register")]

            public async Task<IActionResult> Register(RegisterUserDTO dto)

            {

                var result = await _authService.Register(dto);

                if (!result)

                    return BadRequest("User registration alredy exit");

                return Ok("User registered successfully");

            }

            // LOGIN USER

            [HttpPost("login")]

            public async Task<IActionResult> Login(LoginDTO dto)

            {

                var token = await _authService.Login(dto);

                if (token == null)

                    return Unauthorized("Invalid email or password");

                return Ok(new { token });

            }

        }

    }
}
 