using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

using WarehouseProject.DTOs;
using WarehouseProject.DTOs.itemDtos;

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

    // ✅ GET ALL

    [HttpGet]

    public async Task<IActionResult> GetAll()

    {

        return Ok(await _service.GetAll());

    }

    // ✅ GET BY ID

    [HttpGet("{id}")]

    public async Task<IActionResult> GetById(int id)

    {

        var result = await _service.GetById(id);

        if (result == null) return NotFound();

        return Ok(result);

    }

    // ✅ CREATE (Admin)

    [HttpPost]

    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> Create(CreateItemDTO dto)

    {

        var result = await _service.Create(dto);

        if (result == null)

            return BadRequest("Item with same SKU already exists");

        return Ok(result);

    }

    // ✅ UPDATE (Admin)

    [HttpPut("{id}")]

    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> Update(int id, UpdateItemDTO dto)

    {

        var result = await _service.Update(id, dto);

        if (result == null)

            return BadRequest("Duplicate SKU or Item not found");

        return Ok(result);

    }

    // ✅ DELETE (Admin)

    [HttpDelete("{id}")]

    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> Delete(int id)

    {

        var result = await _service.Delete(id);

        if (!result)

            return NotFound();

        return Ok("Deleted successfully");

    }

}
