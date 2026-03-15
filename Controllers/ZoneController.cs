using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseProject.DTOs;
using WarehouseProject.Services;
namespace WarehouseProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ZoneController : ControllerBase
    {
        private readonly IZoneService _service;
        public ZoneController(IZoneService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var zones = await _service.GetAll();
            return Ok(zones);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(ZoneDTO dto)
        {
            var zone = await _service.Create(dto);
            return Ok(zone);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);
            if (!result)
                return NotFound();
            return Ok("Zone deleted");
        }
    }
}