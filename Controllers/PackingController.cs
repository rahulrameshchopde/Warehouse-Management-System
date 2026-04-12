using Microsoft.AspNetCore.Mvc;
using WarehouseProject.Services;
using Microsoft.AspNetCore.Authorization;
using WarehouseProject.DTOs.Outbound;
namespace WarehouseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Logistic")]

   

    public class PackingController : ControllerBase

    {

        private readonly IPackingUnitService _service;

        public PackingController(IPackingUnitService service)

        {

            _service = service;

        }

        [HttpPost]
        
        public async Task<IActionResult> Create(PackingCreateDto dto)

        {

            var result = await _service.CreateAsync(dto);

            return Ok(result);

        }

        [HttpGet]

        public async Task<IActionResult> GetAll()

        {

            return Ok(await _service.GetAllAsync());

        }
        [HttpPut("status/{id}")]

        public async Task<IActionResult> UpdateStatus(int id, PackingStatusUpdateDto dto)

        {

            var result = await _service.UpdateAsync(id, dto);

            if (result == null)

                return NotFound();

            return Ok(result);

        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)

        {

            var result = await _service.DeleteAsync(id);

            if (!result)

                return NotFound();

            return Ok();

        }



    }
}
 