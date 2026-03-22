using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WarehouseProject.Data;
using WarehouseProject.DTOs.Register;
using WarehouseProject.Models;
namespace WarehouseProject.Services;

public class AuthService : IAuthService
{
    private readonly WarehouseDBContext _context;
    private readonly IConfiguration _config;
    public AuthService(WarehouseDBContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public async Task<IEnumerable<UserModel>> GetAllUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<UserModel> GetUserById(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    // REGISTER USER
    public async Task<bool> Register(RegisterUserDTO dto)

    {
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(x => x.Email == dto.Email);
        if (existingUser != null)
        {
            return false; // user already exists
        }
        var user = new UserModel
        {

            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone,
            Password = dto.Password,
            Role = dto.Role
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return true;
    }

    // LOGIN USER
    public async Task<string> Login(LoginDTO loginDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email);

        if (user == null)
            return "User not found ";

        if (user.Password != loginDto.Password)
            return "Invalid password";
        return JWTTokenGenerator(user);
    }

    public async Task<bool> UpdateUser(int id, RegisterUserDTO dto)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return false;
        // ✅ Update only required fields
        user.Name = dto.Name;
        user.Role = dto.Role;
        // ❌ DO NOT update password unless needed
        // user.Password = dto.Password;  ← avoid this
        await _context.SaveChangesAsync();
        return true;
    }


    public async Task<bool> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return false;
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }


    // GENERATE JWT TOKEN
    public string JWTTokenGenerator(UserModel user)
    {
       

        var key = new SymmetricSecurityKey(
          Encoding.UTF8.GetBytes(_config["Jwt:Key"])
      );
       
        var claims = new[]
        {
           new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
           new Claim(ClaimTypes.Email, user.Email),
           new Claim(ClaimTypes.Role, user.Role)
       };

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            claims: claims,
            issuer: "WarehouseProAPI",
            audience: "WarehouseProUsers",
            expires: DateTime.Now.AddHours(2),
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}