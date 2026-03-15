using System.ComponentModel.DataAnnotations;

namespace WarehouseProject.Models
{
    public class UserModel
    {
        [Key]
        public int UserID { get; set; }
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
        public ICollection<AuditLogModel> AuditLogs { get; set; }
        public ICollection<NotificationModel> Notifications { get; set; }
    }
}
