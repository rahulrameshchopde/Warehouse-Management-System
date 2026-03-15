namespace WarehouseProject.DTOs
{
    public class AuditLogResponseDTO
    {
        public int AuditID { get; set; }
        public int UserID { get; set; }
        public string Action { get; set; }
        public string Resource { get; set; }
        public string Metadata { get; set; }
        public DateTime Timestamp { get; set; }
    }
}