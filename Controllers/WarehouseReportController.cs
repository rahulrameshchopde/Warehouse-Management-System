using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using WarehouseProject.DTOs.Notification;
using WarehouseProject.Models;

using WarehouseProject.Services;

namespace WarehouseProject.Controllers

{

    [Route("api/[controller]")]

    [ApiController]

    [Authorize(Roles = "Admin,Supervisor")]

    public class WarehouseReportController : ControllerBase

    {

        private readonly IWarehouseReportService _service;

        public WarehouseReportController(IWarehouseReportService service)

        {

            _service = service;

        }

        [HttpGet]

        public async Task<IActionResult> GetAll()

        {

            var reports = await _service.GetAllAsync();

            return Ok(reports);

        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)

        {

            var report = await _service.GetByIdAsync(id);

            if (report == null)

                return NotFound();

            return Ok(report);

        }

        [HttpPost]

        [HttpPost]

        public async Task<IActionResult> Create(WarehouseReportDTO dto)

        {

            var report = new WarehouseReportModel

            {

                

                Metrics = dto.Metrics,

                GeneratedDate = DateTime.UtcNow

            };

            var result = await _service.CreateAsync(report);

            return Ok(result);

        }

    }

}
