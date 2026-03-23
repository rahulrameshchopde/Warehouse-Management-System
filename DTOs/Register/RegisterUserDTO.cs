using System.ComponentModel.DataAnnotations;

namespace WarehouseProject.DTOs.Register
{
    public class RegisterUserDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Role { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        
        public string Password { get; set; }
    }
}
