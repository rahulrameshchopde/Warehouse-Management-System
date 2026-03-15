using Microsoft.EntityFrameworkCore;
using WarehouseProject.Data;
using WarehouseProject.DTOs;
using WarehouseProject.Models;
namespace WarehouseProject.Services
{
    public class NotificationService : INotificationService
    {
        private readonly WarehouseDBContext _context;
        public NotificationService(WarehouseDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<NotificationResponseDTO>> GetAllAsync()
        {
            return await _context.Notifications
                .Include(n => n.User)
                .Select(n => new NotificationResponseDTO
                {
                    NotificationID = n.NotificationID,
                    UserID = n.UserID,
                    Message = n.Message,
                    Category = n.Category,
                    Status = n.Status,
                    CreatedDate = n.CreatedDate,
                    User = new UserResponseDTO
                    {
                        UserID = n.User.UserID,
                        Name = n.User.Name,
                        Email = n.User.Email,
                        Phone = n.User.Phone,
                        Role = n.User.Role
                    }
                })
                .ToListAsync();
        }
        public async Task<NotificationModel> CreateAsync(NotificationModel notification)
        {
            notification.CreatedDate = DateTime.UtcNow;
            notification.Status = "Unread";
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
            return notification;
        }
    }
}