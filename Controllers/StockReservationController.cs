using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using WarehouseProject.DTOs.ReplenishmentDtos;
using WarehouseProject.Services;

namespace WarehouseProject.Controllers

{

    [Route("api/[controller]")]

    [ApiController]

    [Authorize(Roles = "Operator")]

    public class StockReservationController : ControllerBase

    {

        private readonly IStockReservationService _service;

        public StockReservationController(IStockReservationService service)

        {

            _service = service;

        }

        [HttpPost]

        public async Task<IActionResult> Create(StockReservationDTO dto)

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

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)

        {

            var result = await _service.Delete(id);

            if (!result) return NotFound();

            return Ok("Deleted");

        }

    }

}
