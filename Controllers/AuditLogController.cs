using Microsoft.AspNetCore.Mvc;

using WarehouseProject.Services.AuditLogs;

namespace WarehouseProject.Controllers

{

    [Route("api/[controller]")]

    [ApiController]

    public class AuditLogController : ControllerBase

    {

        private readonly IAuditLogService _service;

        public AuditLogController(IAuditLogService service)

        {

            _service = service;

        }

        [HttpGet]

        public async Task<IActionResult> GetAll()

        {

            var logs = await _service.GetAll();

            return Ok(logs);

        }

        [HttpGet("{id}")]

        public async Task<IActionResult> Get(int id)

        {

            var log = await _service.GetById(id);

            if (log == null)

                return NotFound();

            return Ok(log);

        }

    }

}
