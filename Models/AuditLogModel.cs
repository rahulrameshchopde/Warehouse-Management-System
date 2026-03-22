using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WarehouseProject.Models;
public class AuditLogModel
{
    [Key]
    public int AuditID { get; set; }
    [Required]
    public int UserID { get; set; }
    [Required]
    public string Action { get; set; } = string.Empty;
    [Required]
    public string Resource { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;  // ✅ auto time
    public string? Metadata { get; set; }
    [ForeignKey("UserID")]
    public UserModel User { get; set; }
}