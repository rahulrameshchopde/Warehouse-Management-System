using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

using WarehouseProject.DTOs.PutAwayTaskDTOs;

[Route("api/[controller]")]

[ApiController]

public class PutAwayTaskController : ControllerBase

{

    private readonly IPutAwayTaskService _service;

    public PutAwayTaskController(IPutAwayTaskService service)

    {

        _service = service;

    }

    // ✅ GET ALL

    [HttpGet]

    [Authorize(Roles = "Admin,Supervisor,Operator")]

    public async Task<IActionResult> GetAll()

    {

        var data = await _service.GetAll();

        return Ok(data);

    }

    // ✅ GET BY ID

    [HttpGet("{id}")]

    [Authorize(Roles = "Admin,Supervisor,Operator")]

    public async Task<IActionResult> GetById(int id)

    {

        var data = await _service.GetById(id);

        if (data == null)

            return NotFound("PutAway Task not found ❌");

        return Ok(data);

    }

    // 🔥 MAIN PUTAWAY (CREATE + UPDATE + INVENTORY)

    [HttpPost]

    [Authorize(Roles = "Admin,Supervisor")]

    public async Task<IActionResult> PutAway(CreatePutAwayTaskDTO dto)

    {

        var result = await _service.PutAwayAsync(dto);

        if (result.Contains("Invalid"))

            return BadRequest(result);

        return Ok(result);

    }

    // ✅ UPDATE

    [HttpPut("{id}")]

    [Authorize(Roles = "Admin,Supervisor")]

    public async Task<IActionResult> Update(int id, UpdatePutAwayTaskDTO dto)

    {

        var data = await _service.Update(id, dto);

        if (data == null)

            return NotFound("Task not found ❌");

        return Ok(data);

    }

    // ✅ DELETE

    [HttpDelete("{id}")]

    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> Delete(int id)

    {

        var result = await _service.Delete(id);

        if (!result)

            return NotFound("Task not found ❌");

        return Ok("Deleted Successfully ✅");

    }

}
