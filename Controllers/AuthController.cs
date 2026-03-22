using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;
using WarehouseProject.DTOs.Register;

namespace WarehouseProject.Controllers

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

        // ✅ REGISTER (Public)

        [HttpPost("register")]

        public async Task<IActionResult> Register(RegisterUserDTO dto)
        {
            var result = await _authService.Register(dto);

            if (!result)

                return BadRequest("User already exists");

            return Ok("User registered successfully");

        }


        // ✅ LOGIN (Public)
        [HttpPost("login")]

        public async Task<IActionResult> Login(LoginDTO dto)

        {
            var token = await _authService.Login(dto);

            if (token == null)

                return Unauthorized("Invalid credentials");

            return Ok(new { token });
        }

        // ✅ GET ALL USERS (Admin only)

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _authService.GetAllUsers();
            return Ok(users);
        }

        // ✅ GET USER BY ID (Admin only)

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _authService.GetUserById(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // ✅ UPDATE USER (Admin only)

        [HttpPut("{id}")]

        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> UpdateUser(int id, RegisterUserDTO dto)

        {

            var result = await _authService.UpdateUser(id, dto);

            if (!result)

                return NotFound("User not found");

            return Ok("User updated successfully");

        }

        // ✅ DELETE USER (Admin only)

        [HttpDelete("{id}")]

        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteUser(int id)

        {
            var result = await _authService.DeleteUser(id);

            if (!result)

                return NotFound("User not found");

            return Ok("User deleted successfully");

        }

    }

}
