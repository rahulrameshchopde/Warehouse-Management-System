using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehousePro.API.DTOs.Outbound;
using WarehousePro.API.Services.Interfaces;
using WarehouseProject.DTOs.Outbound;
using WarehouseProject.Services;


namespace WarehouseProject.Controllers

{
    [ApiController]

    [Route("api/[controller]")]

    [Authorize]

    public class PickTaskController : ControllerBase

    {

        private readonly IPickTaskService _service;

        public PickTaskController(IPickTaskService service)

        {

            _service = service;

        }

        // ✅ CREATE PICK TASK (Manual)

        [HttpPost]

        [Authorize(Roles = "Admin,Operator")]

        public async Task<IActionResult> Create(PickTaskCreateDto dto)

        {

            var result = await _service.CreatePickAsync(dto);

            return Ok(result);

        }

        // ✅ AUTO CREATE FROM ORDER

        [HttpPost("auto/{orderId}")]

        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> AutoCreate(int orderId)

        {

            await _service.AutoCreateFromOrder(orderId);

            return Ok("PickTasks created automatically");

        }

        // ✅ GET ALL PICK TASKS

        [HttpGet]

        [Authorize(Roles = "Admin,Supervisor,Operator")]

        public async Task<IActionResult> GetAll()

        {

            var result = await _service.GetAllAsync();

            return Ok(result);

        }



        [HttpDelete("{id}")]

        public IActionResult Delete(int id)

        {

            var result = _service.DeletePickTask(id);

            if (!result)

                return NotFound();

            return Ok("Deleted");

        }



        // ✅ UPDATE STATUS (AUTO INVENTORY REDUCE HERE 🔥)

        [HttpPut("status/{id}")]

        [Authorize(Roles = "Admin,Operator")]

        public async Task<IActionResult> UpdateStatus(int id, PickTaskUpdateDto dto)

        {

            var result = await _service.UpdateStatusAsync(id, dto);

            if (result == null)

                return NotFound("PickTask not found");

            return Ok(result);

        }

    }
}
 