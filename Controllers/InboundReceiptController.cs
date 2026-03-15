using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

using WarehouseProject.DTOs;

using WarehouseProject.Services;

namespace WarehouseProject.Controllers

{

    [Route("api/[controller]")]

    [ApiController]

    [Authorize(Roles = "Operator,Supervisor")]

    public class InboundReceiptController : ControllerBase

    {

        private readonly IInboundReceiptService _service;

        public InboundReceiptController(IInboundReceiptService service)

        {

            _service = service;

        }

        [HttpPost]

        public async Task<IActionResult> Create(InboundReceiptDTO dto)

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

            return Ok("Deleted");

        }

    }

}
