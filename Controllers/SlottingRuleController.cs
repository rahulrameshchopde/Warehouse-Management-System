using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseProject.Models;
using WarehouseProject.Services;
namespace WarehouseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "InventoryPlanner")]
    public class SlottingRuleController : ControllerBase
    {
        private readonly ISlottingRuleService _service;
        public SlottingRuleController(ISlottingRuleService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rules = await _service.GetAllAsync();
            return Ok(rules);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var rule = await _service.GetByIdAsync(id);
            if (rule == null)
                return NotFound();
            return Ok(rule);
        }
        [HttpPost]
        public async Task<IActionResult> Create(SlottingRuleModel rule)
        {
            var created = await _service.CreateAsync(rule);
            return Ok(created);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SlottingRuleModel rule)
        {
            var updated = await _service.UpdateAsync(id, rule);
            if (updated == null)
                return NotFound();
            return Ok(updated);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound();
            return Ok("Deleted successfully");
        }
    }
}