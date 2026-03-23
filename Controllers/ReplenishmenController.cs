using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseProject.DTOs.ReplenishmentDtos;
using WarehouseProject.Services.Replenishment_Slotting;
namespace WarehouseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "InventoryPlanner")]
    public class ReplenishmentController : ControllerBase
    {
        private readonly IReplenishmentService _service;
        public ReplenishmentController(IReplenishmentService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> Create(ReplenishmentDTO dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }
        [HttpPut("complete/{id}")]
        public async Task<IActionResult> Complete(int id)
        {
            var result = await _service.CompleteTask(id);
            return Ok(result);
        }
    }
}