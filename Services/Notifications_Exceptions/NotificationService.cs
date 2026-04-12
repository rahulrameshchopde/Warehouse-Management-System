using Microsoft.EntityFrameworkCore;
using WarehousePro.API.Models.Enums;
using WarehouseProject.Data;
using WarehouseProject.DTOs;
using WarehouseProject.DTOs.Notification;
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

        // ✅ CREATE

        public async Task CreateAsync(int userId, string message, NotificationCategory category)

        {

            var notification = new NotificationModel

            {

                UserID = userId,

                Message = message,

                Category = category

            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

        }

        // ✅ GET BY USER

        public async Task<List<NotificationResponseDto>> GetByUserAsync(int userId)

        {

            return await _context.Notifications

                .Where(x => x.UserID == userId)

                .OrderByDescending(x => x.CreatedDate)

                .Select(x => new NotificationResponseDto

                {

                    NotificationID = x.NotificationID,

                    Message = x.Message,

                    Category = x.Category.ToString(),

                    Status = x.Status.ToString(),

                    CreatedDate = x.CreatedDate

                })

                .ToListAsync();

        }

        // ✅ MARK AS READ

        public async Task<bool> MarkAsReadAsync(int id)

        {

            var data = await _context.Notifications.FindAsync(id);

            if (data == null) return false;

            data.Status = NotificationStatus.Read;

            await _context.SaveChangesAsync();

            return true;

        }

    }
}