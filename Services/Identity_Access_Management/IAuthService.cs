using WarehouseProject.DTOs;
using WarehouseProject.Models;

namespace WarehouseProject.Services.Identity_Access_Management
{
    public interface IAuthService
    {
        Task<bool> Register (RegisterUserDTO dto);
        Task<string> Login (LoginDTO dto);

        string JWTTokenGenerator(UserModel user);
    }
}
 