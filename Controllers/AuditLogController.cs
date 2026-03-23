using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

using WarehouseProject.Data;

namespace WarehouseProject.Controllers

{

    [ApiController]

    [Route("api/[controller]")]

    public class AuditLogController : ControllerBase

    {

        private readonly WarehouseDBContext _context;

        public AuditLogController(WarehouseDBContext context)

        {

            _context = context;

        }

        // ✅ GET ALL AUDIT LOGS

        [HttpGet]

        public async Task<IActionResult> GetAllLogs()

        {

            var logs = await _context.AuditLogs

                .Include(x => x.User) // get user info

                .Select(x => new

                {

                    x.AuditID,

                    x.UserID,

                    UserName = x.User.Name,

                    x.Action,

                    x.Resource,

                    x.Metadata,

                    x.Timestamp

                })

                .ToListAsync();

            return Ok(logs);

        }

        // ✅ GET LOG BY ID

        [HttpGet("{id}")]

        public async Task<IActionResult> GetLogById(int id)

        {

            var log = await _context.AuditLogs

                .Include(x => x.User)

                .Where(x => x.AuditID == id)

                .Select(x => new

                {

                    x.AuditID,

                    x.UserID,

                    UserName = x.User.Name,

                    x.Action,

                    x.Resource,

                    x.Metadata,

                    x.Timestamp

                })

                .FirstOrDefaultAsync();

            if (log == null)

                return NotFound();

            return Ok(log);

        }

    }

}
