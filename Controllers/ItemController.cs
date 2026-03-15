using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;

using WarehouseProject.DTOs;

using WarehouseProject.Services.Inventory_Stock_Control;

namespace WarehouseProject.Controllers

{

    [Route("api/[controller]")]

    [ApiController]

    [Authorize]

    public class ItemController : ControllerBase

    {

        private readonly IItemService _service;

        public ItemController(IItemService service)

        {

            _service = service;

        }

        [HttpGet]

        public async Task<IActionResult> GetAll()

        {

            var items = await _service.GetAll();

            return Ok(items);

        }

        [HttpGet("{id}")]

        public async Task<IActionResult> Get(int id)

        {

            var item = await _service.GetById(id);

            if (item == null)

                return NotFound();

            return Ok(item);

        }

        [HttpPost]

        [Authorize(Roles = "Admin,InventoryPlanner")]

        public async Task<IActionResult> Create(ItemDTO dto)

        {

            var item = await _service.Create(dto);

            return Ok(item);

        }

        [HttpPut("{id}")]

        [Authorize(Roles = "Admin, InventoryPlanner")]

        public async Task<IActionResult> Update(int id, ItemDTO dto)

        {

            var item = await _service.Update(id, dto);

            if (item == null)

                return NotFound();

            return Ok(item);

        }

        [HttpDelete("{id}")]

        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int id)

        {

            var result = await _service.Delete(id);

            if (!result)

                return NotFound();

            return Ok("Item deleted");

        }

    }

}
