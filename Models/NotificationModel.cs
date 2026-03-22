using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WarehousePro.API.Models.Enums;

namespace WarehouseProject.Models
{
    public class NotificationModel
    {
        [Key]
        public int NotificationID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public string Message { get; set; }
        public NotificationCategory Category { get; set; }
        public NotificationStatus Status { get; set; } = NotificationStatus.Unread;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [ForeignKey("UserID")]
        public UserModel User { get; set; }
    }
}
