using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using WarehousePro.API.DTOs.Warehouse;
using WarehouseProject.Models;
using WarehouseProject.Services;


namespace WarehouseProject.Controllers

{

    [Route("api/[controller]")]

    [ApiController]

    public class WarehouseController : ControllerBase

    {

        private readonly IWarehouseService _service;

        public WarehouseController(IWarehouseService service)

        {

            _service = service;

        }

        [HttpGet]

        [Authorize(Roles = "Admin,Operator")]

        public async Task<IActionResult> GetAll()

        {

            return Ok(await _service.GetAllAsync());

        }

        [HttpGet("{id}")]

        [Authorize(Roles = "Admin,Operator")]

        public async Task<IActionResult> GetById(int id)

        {

            var result = await _service.GetByIdAsync(id);

            if (result == null)

                return NotFound();

            return Ok(result);

        }



        [HttpPost]

        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Create(WarehouseCreateDto dto)

        {

            var result = await _service.CreateAsync(dto);

            return Ok(result);

        }

        [HttpPut("{id}")]

        [Authorize(Roles = "Admin")]




        public async Task<IActionResult> Update(int id, WarehouseUpdateDto dto)

        {

            var result = await _service.UpdateAsync(id, dto);

            return Ok(result);

        }



        [HttpDelete("{id}")]

        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int id)

        {

            var result = await _service.DeleteAsync(id);

            if (!result)

                return NotFound();

            return Ok("Deleted Successfully");

        }

    }


}