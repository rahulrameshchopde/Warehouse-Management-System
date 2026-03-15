using Microsoft.AspNetCore.Mvc;
using WarehouseProject.Services;
using WarehouseProject.DTOs;
using Microsoft.AspNetCore.Authorization;
namespace WarehouseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Operator")]
    public class PackingUnitController : ControllerBase
    {
        private readonly IPackingUnitService _service;
        public PackingUnitController(IPackingUnitService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> Create(PackingUnitDTO dto)
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
        [HttpPut("complete/{packId}")]
        public async Task<IActionResult> CompletePacking(int packId)
        {
            var result = await _service.CompletePacking(packId);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}