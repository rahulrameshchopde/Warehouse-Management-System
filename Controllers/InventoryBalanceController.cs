using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseProject.DTOs;
using WarehouseProject.Services.Inventory_Stock_Control;
namespace WarehouseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "InventoryPlanner,Supervisor")]
    public class InventoryBalanceController : ControllerBase
    {
        private readonly IInventoryBalanceService _service;
        public InventoryBalanceController(IInventoryBalanceService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> Create(InventoryBalanceDTO dto)
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetById(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);
            if (!result)
                return NotFound();
            return Ok("Deleted Successfully");
        }
    }
}