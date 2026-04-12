using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WarehouseProject.Models;

using WarehouseProject.Services;

namespace WarehouseProject.Controllers

{

    [Route("api/[controller]")]

    [ApiController]
    [Authorize(Roles = "Admin,Supervisor,Operator")]

    public class BinLocationController : ControllerBase

    {

        private readonly IBinLocationService _service;

        // ✅ Inject INTERFACE (IMPORTANT)

        public BinLocationController(IBinLocationService service)

        {

            _service = service;

        }

        // ✅ GET ALL

        [HttpGet]

        public async Task<IActionResult> GetAll()

        {

            var result = await _service.GetAll();

            return Ok(result);

        }

        // ✅ GET BY ID

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)

        {

            var result = await _service.GetById(id);

            if (result == null)

                return NotFound("Data not found");

            return Ok(result);

        }

        // ✅ CREATE

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] BinLocationModel model)

        {

            if (!ModelState.IsValid)

                return BadRequest(ModelState);

            var result = await _service.Create(model);

            return Ok(result);

        }

        // ✅ UPDATE

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, [FromBody] BinLocationModel model)

        {

            if (!ModelState.IsValid)

                return BadRequest(ModelState);

            var result = await _service.Update(id, model);

            if (result == null)

                return NotFound("Data not found");

            return Ok(result);

        }

        // ✅ DELETE

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)

        {

            var result = await _service.Delete(id);

            if (!result)

                return NotFound("Data not found");

            return Ok("Deleted Successfully");

        }

    }

}
