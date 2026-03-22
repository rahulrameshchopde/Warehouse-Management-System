using WarehouseProject.DTOs.Register;
using WarehouseProject.Models;

public interface IAuthService
{
    
    Task<bool> Register(RegisterUserDTO dto);
    Task<string> Login(LoginDTO dto);
    string JWTTokenGenerator(UserModel user);
    
    Task<IEnumerable<UserModel>> GetAllUsers();
    Task<UserModel> GetUserById(int id);
    Task<bool> UpdateUser(int id, RegisterUserDTO dto); // using same DTO
    Task<bool> DeleteUser(int id);
}