using WarehouseProject.DTOs.Notification;
public interface IAuditLogService
{
    Task AddLog(string action, string resource, string? metadata);
}