using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

using WarehouseProject.DTOs.Register;

[ApiController]

[Route("api/[controller]")]

public class AuthController : ControllerBase

{

    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)

    {

        _authService = authService;

    }

    // ✅ LOGIN (PUBLIC)

    [HttpPost("login")]

    public async Task<IActionResult> Login(LoginDTO dto)

    {

        var token = await _authService.Login(dto);

        if (token == null)

            return Unauthorized("Invalid credentials");

        return Ok(new {token= token,role=_authService.UserRole });

    }

    // 🔥 SUPER ADMIN → CREATE ANY USER

    [Authorize(Roles = "Admin")]

    [HttpPost("Register")]

    public async Task<IActionResult> CreateUser(RegisterUserDTO dto)

    {

        var result = await _authService.Register(dto);

        if (!result)

            return BadRequest( new { message = "User exists" });

        return Ok(new {message= "User created successfully" });

    }

    // 🔥 GET USERS

    [Authorize(Roles = "Admin")]

    [HttpGet]

    public async Task<IActionResult> GetUsers()

    {

        return Ok(await _authService.GetAllUsers());

    }

    // 🔥 UPDATE

    [Authorize(Roles = "Admin")]

    [HttpPut("update/{id}")]

    public async Task<IActionResult> UpdateUser(int id, UpdateUserDTO dto)

    {

        var result = await _authService.UpdateUser(id, dto);

        if (!result)

            return NotFound("User not found");

        return Ok("User updated successfully");

    }

    // 🔥 DELETE

    [Authorize(Roles = "Admin")]

    [HttpDelete("{id}")]

    public async Task<IActionResult> DeleteUser(int id)

    {

        return Ok(await _authService.DeleteUser(id));

    }

}
