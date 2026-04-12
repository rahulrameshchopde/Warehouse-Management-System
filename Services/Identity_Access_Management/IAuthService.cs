using WarehouseProject.DTOs.Register;

using WarehouseProject.Models;

public interface IAuthService

{
    public string UserRole { get; set; }
    Task<bool> Register(RegisterUserDTO dto);
    Task<string> Login(LoginDTO dto);
    

    Task<IEnumerable<UserModel>> GetAllUsers();

    Task<UserModel> GetUserById(int id);
    Task<bool> UpdateUser(int id, UpdateUserDTO dto);
    Task<bool> DeleteUser(int id);

}
