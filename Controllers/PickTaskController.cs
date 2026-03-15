using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

using WarehouseProject.DTOs;

using WarehouseProject.Services;
using WarehouseProject.Services.Picking_Packing_Dispatch;

namespace WarehouseProject.Controllers

{

    [Route("api/[controller]")]

    [ApiController]

    [Authorize(Roles = "Operator")]

    public class PickTaskController : ControllerBase

    {

        private readonly IPickTaskService _service;

        public PickTaskController(IPickTaskService service)

        {

            _service = service;

        }

        [HttpPost]

        public async Task<IActionResult> Create(PickTaskDTO dto)

        {

            var result = await _service.Create(dto);

            return Ok(result);

        }

        [HttpGet]

        public async Task<IActionResult> GetAll()

        {

            return Ok(await _service.GetAll());

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

            return Ok("Deleted");

        }

    }

}
