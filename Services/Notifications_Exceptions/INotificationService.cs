using WarehouseProject.DTOs;
using WarehouseProject.Models;
namespace WarehouseProject.Services
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationResponseDTO>> GetAllAsync();
        Task<NotificationModel> CreateAsync(NotificationModel notification);
        
    }
}