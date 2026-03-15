using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseProject.Models
{
    public class AuditLogModel
    {
        [Key]
        public int AuditID { get; set; }
        public int UserID { get; set; }
        public string Action { get; set; }
        public string Resource { get; set; }
        public DateTime Timestamp { get; set; }
        public string Metadata { get; set; }
        
        [ForeignKey("UserID")]
        public UserModel User { get; set; }
    }
}
