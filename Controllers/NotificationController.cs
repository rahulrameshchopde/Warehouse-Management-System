using Microsoft.AspNetCore.Mvc;
using WarehouseProject.Models;
using WarehouseProject.Services;
namespace WarehouseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _service;
        public NotificationController(INotificationService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var notifications = await _service.GetAllAsync();
            return Ok(notifications);
        }
        [HttpPost]
        public async Task<IActionResult> Create(NotificationModel notification)
        {
            var result = await _service.CreateAsync(notification);
            return Ok(result);
        }
    }
}