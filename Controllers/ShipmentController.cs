using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WarehouseProject.DTOs.Outbound;
namespace WarehouseProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentService _service;
        public ShipmentController(IShipmentService service)
        {
            _service = service;
        }
        // ✅ CREATE
        [HttpPost]
        [Authorize(Roles = "Admin,Operator")]
        public async Task<IActionResult> Create(ShipmentCreateDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }
        // ✅ GET ALL
        [HttpGet]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }
        // ✅ UPDATE
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, ShipmentUpdateDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            if (result == null)
                return NotFound("Shipment not found");
            return Ok(result);
        }
    }
}