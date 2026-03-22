using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehousePro.API.DTOs.Zone;
using WarehouseProject.Services;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]

[ApiController]

public class ZoneController : ControllerBase

{

    private readonly IZoneService _service;

    public ZoneController(IZoneService service)

    {

        _service = service;

    }

    // 🔹 GET ALL

    [HttpGet]

    [Authorize]

    public async Task<IActionResult> GetAll()

    {

        var result = await _service.GetAllAsync();

        return Ok(result);

    }

    // 🔹 GET BY ID

    [HttpGet("{id}")]

    [Authorize]

    public async Task<IActionResult> GetById(int id)

    {

        var result = await _service.GetByIdAsync(id);

        if (result == null)

            return NotFound();

        return Ok(result);

    }

    // 🔹 CREATE

    [HttpPost]

    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> Create(ZoneCreateDto dto)

    {

        var result = await _service.CreateAsync(dto);

        return Ok(result);

    }

    // 🔹 UPDATE

    [HttpPut("{id}")]

    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> Update(int id, ZoneUpdateDto dto)

    {

        var updated = await _service.UpdateAsync(id, dto);

        if (!updated)

            return NotFound();

        return Ok("Updated Successfully");

    }

    // 🔹 DELETE

    [HttpDelete("{id}")]

    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> Delete(int id)

    {

        var deleted = await _service.DeleteAsync(id);

        if (!deleted)

            return NotFound();

        return Ok("Deleted Successfully");

    }

}
