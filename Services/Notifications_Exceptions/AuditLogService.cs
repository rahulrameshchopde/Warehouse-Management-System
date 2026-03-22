using Microsoft.EntityFrameworkCore;
using WarehouseProject.Data;
using WarehouseProject.DTOs.Notification;
namespace WarehouseProject.Services.AuditLogs
{
    public class AuditLogService : IAuditLogService
    {
        private readonly WarehouseDBContext _context;
        public AuditLogService(WarehouseDBContext context)
        {
            _context = context;
        }
        public async Task<List<AuditLogResponseDTO>> GetAll()
        {
            return await _context.AuditLogs
                .Select(x => new AuditLogResponseDTO
                {
                    AuditID = x.AuditID,
                    UserID = x.UserID,
                    Action = x.Action,
                    Resource = x.Resource,
                    Metadata = x.Metadata,
                    Timestamp = x.Timestamp
                })
                .ToListAsync();
        }
        public async Task<AuditLogResponseDTO> GetById(int id)
        {
            var log = await _context.AuditLogs.FindAsync(id);
            if (log == null)
                return null;
            return new AuditLogResponseDTO
            {
                AuditID = log.AuditID,
                UserID = log.UserID,
                Action = log.Action,
                Resource = log.Resource,
                Metadata = log.Metadata,
                Timestamp = log.Timestamp
            };
        }
    }
}