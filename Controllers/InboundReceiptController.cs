using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using WarehouseProject.DTOs.InboundReceipt;

[Route("api/[controller]")]

[ApiController]

[Authorize]

public class InboundReceiptController : ControllerBase

{

    private readonly IInboundReceiptService _service;

    public InboundReceiptController(IInboundReceiptService service)

    {

        _service = service;

    }

    // GET ALL

    [HttpGet]
    [Authorize(Roles = "Admin, Supervisor")]

    public async Task<IActionResult> GetAll()

    {

        return Ok(await _service.GetAll());

    }

    // GET BY ID

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin, Supervisor , InventoryPlanner")]

    public async Task<IActionResult> GetById(int id)

    {

        var result = await _service.GetById(id);

        if (result == null) return NotFound();

        return Ok(result);

    }

    // CREATE (Admin)

    [HttpPost]

    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> Create(CreateInboundReceiptDTO dto)

    {

        var result = await _service.Create(dto);

        if (result == null)

            return BadRequest("ReferenceNo already exists");

        return Ok(result);

    }

    // UPDATE (Admin)

    [HttpPut("{id}")]

    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> Update(int id, UpdateInboundReceiptDTO dto)

    {

        var result = await _service.Update(id, dto);

        if (result == null)

            return NotFound();

        return Ok(result);

    }

    // DELETE (Admin)

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
