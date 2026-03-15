
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WarehouseProject.Controllers
{
    using global::WarehouseProject.DTOs;
    using global::WarehouseProject.Services.Warehouse_Layout_Location_Management;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;


    namespace WarehouseProject.Controllers

    {

        [ApiController]

        [Route("api/[controller]")]

        public class WarehouseController : ControllerBase

        {

            private readonly IWarehouseService _service;

            public WarehouseController(IWarehouseService service)

            {

                _service = service;

            }

            [HttpGet]

            public async Task<IActionResult> GetAll()

            {

                var warehouses = await _service.GetAll();

                return Ok(warehouses);

            }

            [HttpGet("{id}")]

            public async Task<IActionResult> Get(int id)

            {

                var warehouse = await _service.GetById(id);

                if (warehouse == null)

                    return NotFound();

                return Ok(warehouse);

            }

            [Authorize(Roles = "Admin")]
            [HttpPost("Create")]

            public async Task<IActionResult> Create(WarehouseDTO dto)

            {

                var warehouse = await _service.Create(dto);

                return Ok(warehouse);

            }

           [Authorize(Roles = "Admin")]

            [HttpDelete("{id}")]

            public async Task<IActionResult> Delete(int id)

            {

                var result = await _service.Delete(id);

                if (!result)

                    return NotFound();

                return Ok("Warehouse deleted");

            }

        }

    }
};
 