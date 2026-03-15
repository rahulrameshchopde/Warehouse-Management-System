using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseProject.DTOs;
using WarehouseProject.Services;
namespace WarehouseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Supervisor,Operator")]
    public class PutAwayController : ControllerBase
    {
        private readonly IPutAwayService _service;
        public PutAwayController(IPutAwayService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> Create(PutAwayTaskDTO dto)
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
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}