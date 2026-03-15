using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

using WarehouseProject.DTOs;

using WarehouseProject.Services;

namespace WarehouseProject.Controllers

{

    [ApiController]

    [Route("api/[controller]")]

    [Authorize]

    public class BinLocationController : ControllerBase

    {

        private readonly IBinLocationService _service;

        public BinLocationController(IBinLocationService service)

        {

            _service = service;

        }

        [HttpGet]

        public async Task<IActionResult> GetAll()

        {

            var bins = await _service.GetAll();

            return Ok(bins);

        }

        [Authorize(Roles = "Admin")]

        [HttpPost]

        public async Task<IActionResult> Create(BinLocationDTO dto)

        {

            var bin = await _service.Create(dto);

            return Ok(bin);

        }

        [Authorize(Roles = "Admin")]

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)

        {

            var result = await _service.Delete(id);

            if (!result)

                return NotFound();

            return Ok("Bin deleted");

        }

    }

}
