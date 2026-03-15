using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseProject.Models
{
    public class NotificationModel
    {
        [Key]
        public int NotificationID { get; set; }
        public int UserID { get; set; }
        public string Message { get; set; }
        public string Category { get; set; }
        public string Status { get; set; } = "Unread";
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
       
        [ForeignKey("UserID")]
        public UserModel? User { get; set; }
    }
}
