using WarehouseProject.Data;

using WarehouseProject.Models;

namespace WarehouseProject.Helpers

{

    public class AuditHelper

    {

        private readonly WarehouseDBContext _context;

        public AuditHelper(WarehouseDBContext context)

        {

            _context = context;

        }

        public async Task LogAction(int userId, string action, string resource, string metadata)

        {

            var audit = new AuditLogModel

            {

                UserID = userId,

                Action = action,

                Resource = resource,

                Timestamp = DateTime.UtcNow,

                Metadata = metadata

            };

            _context.AuditLogs.Add(audit);

            await _context.SaveChangesAsync();

        }

    }

}
