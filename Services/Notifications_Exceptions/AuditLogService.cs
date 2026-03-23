using System.Security.Claims;
using WarehouseProject.Data;

public class AuditLogService : IAuditLogService

{

    private readonly WarehouseDBContext _context;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuditLogService(

        WarehouseDBContext context,

        IHttpContextAccessor httpContextAccessor)

    {

        _context = context;

        _httpContextAccessor = httpContextAccessor;

    }

    public async Task AddLog(string action, string resource, string? metadata)

    {

        int userId = 0;

        // 🔥 Get UserID from JWT

        var user = _httpContextAccessor.HttpContext?.User;

        if (user != null && user.Identity.IsAuthenticated)

        {

            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim != null)

            {

                userId = int.Parse(userIdClaim.Value);

            }

        }

        // ⚠️ fallback (for testing only)

        if (userId == 0)

        {

            userId = 1; // default user

        }

        var log = new AuditLogModel

        {

            UserID = userId,

            Action = action,

            Resource = resource,

            Metadata = metadata,

            Timestamp = DateTime.UtcNow

        };

        _context.AuditLogs.Add(log);

        await _context.SaveChangesAsync();

    }

}
