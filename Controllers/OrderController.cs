using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseProject.DTOs.Order;
using WarehouseProject.Services.Order;

[ApiController]

[Route("api/[controller]")]

public class OrderController : ControllerBase

{

    private readonly IOrderService _service;

    public OrderController(IOrderService service)

    {

        _service = service;

    }

    [HttpPost]

    [Authorize(Roles = "Admin,Operator")]

    public async Task<IActionResult> Create(OrderCreateDto dto)

        => Ok(await _service.CreateAsync(dto));

    [HttpGet]

    [Authorize]

    public async Task<IActionResult> GetAll()

        => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]

    [Authorize]

    public async Task<IActionResult> GetById(int id)

    {

        var result = await _service.GetByIdAsync(id);

        return result == null ? NotFound() : Ok(result);

    }

    [HttpPut("{id}")]

    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> Update(int id, OrderUpdateDto dto)

        => await _service.UpdateAsync(id, dto) ? Ok() : NotFound();

    [HttpDelete("{id}")]

    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> Delete(int id)

        => await _service.DeleteAsync(id) ? Ok() : NotFound();

    // 🔥 FLOW APIs

    [HttpPost("start-picking/{id}")]

    [Authorize(Roles = "Operator")]

    public async Task<IActionResult> StartPicking(int id)

        => await _service.StartPicking(id) ? Ok("Picking Started") : NotFound();

    [HttpPost("complete-picking/{id}")]

    [Authorize(Roles = "Operator")]

    public async Task<IActionResult> CompletePicking(int id)

        => await _service.CompletePicking(id) ? Ok("Packed") : NotFound();

    [HttpPost("ship/{id}")]

    [Authorize(Roles = "Supervisor")]

    public async Task<IActionResult> Ship(int id)

        => await _service.Ship(id) ? Ok("Shipped") : NotFound();

    [HttpPost("deliver/{id}")]

    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> Deliver(int id)

        => await _service.Deliver(id) ? Ok("Delivered") : NotFound();

}
