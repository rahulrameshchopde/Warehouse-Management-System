using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseProject.Models;
using WarehouseProject.Services;
namespace WarehouseProject.Controllers
{
    [ApiController]

    [Route("api/[controller]")]

    [Authorize]

    public class NotificationController : ControllerBase

    {

        private readonly INotificationService _service;

        public NotificationController(INotificationService service)

        {

            _service = service;

        }

        // ✅ GET USER NOTIFICATIONS

        [HttpGet("{userId}")]

        public async Task<IActionResult> Get(int userId)

        {

            var data = await _service.GetByUserAsync(userId);

            return Ok(data);

        }

        // ✅ MARK AS READ

        [HttpPut("{id}")]

        public async Task<IActionResult> MarkRead(int id)

        {

            var result = await _service.MarkAsReadAsync(id);

            if (!result)

                return NotFound("Notification not found");

            return Ok("Marked as read");

        }

    }
}