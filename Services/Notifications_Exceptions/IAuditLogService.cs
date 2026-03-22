using WarehouseProject.DTOs.Notification;
namespace WarehouseProject.Services.AuditLogs
{
    public interface IAuditLogService
    {
        Task<List<AuditLogResponseDTO>> GetAll();
        Task<AuditLogResponseDTO> GetById(int id);
    }
}