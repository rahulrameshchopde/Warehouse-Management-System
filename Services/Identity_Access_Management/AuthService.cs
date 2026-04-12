using Microsoft.EntityFrameworkCore;

using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;

using System.Text;

using WarehouseProject.Data;

using WarehouseProject.DTOs.Register;

using WarehouseProject.Models;

public class AuthService : IAuthService

{

    public string UserRole { get; set; }

    private readonly WarehouseDBContext _context;

    private readonly IConfiguration _config;

    public AuthService(WarehouseDBContext context, IConfiguration config)

    {

        _context = context;

        _config = config;

    }

    // ✅ CREATE USER (ONLY FROM CONTROLLER - SUPER ADMIN)

    public async Task<bool> Register(RegisterUserDTO dto)

    {

        var existingUser = await _context.Users

            .FirstOrDefaultAsync(x => x.Email == dto.Email);

        if (existingUser != null)

            return false;

        var allowedRoles = new List<string>

        {

            "Admin",

            "Operator",

            "Supervisor",

            "InventoryPlanner",

            "Logistics"

        };

        var role = allowedRoles.Contains(dto.Role) ? dto.Role : "Operator";

        var user = new UserModel

        {

            Name = dto.Name,

            Email = dto.Email,

            Phone = dto.Phone,

            Password = dto.Password,

            Role = role

        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return true;

    }

    // ✅ LOGIN

    public async Task<string> Login(LoginDTO loginDto)

    {

        var user = await _context.Users

            .FirstOrDefaultAsync(x => x.Email == loginDto.Email);

        if (user == null || user.Password != loginDto.Password)

            return null;

        return GenerateToken(user);

    }

    // ✅ GET USERS

    public async Task<IEnumerable<UserModel>> GetAllUsers()

    {

        return await _context.Users.ToListAsync();

    }

    public async Task<UserModel> GetUserById(int id)

    {

        return await _context.Users.FindAsync(id);

    }

    // ✅ UPDATE

    public async Task<bool> UpdateUser(int id, UpdateUserDTO dto)

    {

        var user = await _context.Users.FindAsync(id);

        if (user == null) return false;

        var allowedRoles = new List<string>

        {

            "Admin",

            "Operator",

            "Supervisor",

            "Planner",

            "Coordinator"

        };

        user.Name = dto.Name;

        user.Phone = dto.Phone;

        user.Role = allowedRoles.Contains(dto.Role) ? dto.Role : user.Role;

        await _context.SaveChangesAsync();

        return true;

    }

    // ✅ DELETE

    public async Task<bool> DeleteUser(int id)

    {

        var user = await _context.Users.FindAsync(id);

        if (user == null) return false;

        _context.Users.Remove(user);

        await _context.SaveChangesAsync();

        return true;

    }

    // 🔐 JWT

    private string GenerateToken(UserModel user)

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
        UserRole = user.Role;

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
