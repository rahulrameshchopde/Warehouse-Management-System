using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseProject.Services.Picking_Packing_Dispatch;
using WarehouseProject.DTOs;
namespace WarehouseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Logistics")]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentService _service;
        public ShipmentController(IShipmentService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> Create(ShipmentDTO dto)
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
    }
}