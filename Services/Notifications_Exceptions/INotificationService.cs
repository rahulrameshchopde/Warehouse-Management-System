using WarehousePro.API.Models.Enums;
using WarehouseProject.DTOs.Notification;
using WarehouseProject.Models;
namespace WarehouseProject.Services
{
    public interface INotificationService
    {
        Task CreateAsync(int userId, string message, NotificationCategory category);
        Task<List<NotificationResponseDto>> GetByUserAsync(int userId);
        Task<bool> MarkAsReadAsync(int id);
    }
}