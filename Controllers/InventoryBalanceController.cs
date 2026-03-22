using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehousePro.API.DTOs.Outbound;
using WarehouseProject.DTOs;
using WarehouseProject.DTOs.InventoryBalanceDTOs;
using WarehouseProject.DTOs.Outbound;
using WarehouseProject.DTOs.PutAwayTaskDTOs;


namespace WarehouseProject.Controllers
{

    [Route("api/[controller]")]

    [ApiController]

    [Authorize]

    public class InventoryBalanceController : ControllerBase

    {

        private readonly IInventoryBalanceService _service;

        public InventoryBalanceController(IInventoryBalanceService service)

        {

            _service = service;

        }

      

        // ✅ GET ALL

        

        [HttpGet]

        [Authorize(Roles = "Admin,InventoryPlanner")]

        public async Task<IActionResult> GetAll()

        {

            var data = await _service.GetAll();

            return Ok(data);

        }

       

        // ✅ GET BY ID

       

        [HttpGet("{id}")]

        [Authorize(Roles = "Admin,InventoryPlanner")]

        public async Task<IActionResult> GetById(int id)

        {

            var result = await _service.GetById(id);

            if (result == null)

                return NotFound();

            return Ok(result);

        }

       

        // ✅ CREATE

       

        [HttpPost]

        [Authorize(Roles = "Admin,InventoryPlanner")]

        public async Task<IActionResult> Create(CreateInventoryBalanceDTO dto)

        {

            var result = await _service.Create(dto);

            if (result == null)

                return BadRequest("Invalid Item/Bin OR Duplicate Entry");

            return Ok(result);

        }

        

        // ✅ UPDATE



        [HttpPut("{id}")]

        [Authorize(Roles = "Admin,Operator,InventoryPlanner")]

        public async Task<IActionResult> Update(int id, UpdateInventoryBalanceDTO dto)

        {

            var result = await _service.Update(id, dto);

            if (result == null)

                return NotFound();

            return Ok(result);

        }

        

        // ✅ DELETE

     

        [HttpDelete("{id}")]

        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int id)

        {

            var result = await _service.Delete(id);

            if (!result)

                return NotFound();

            return Ok("Deleted successfully");

        }

      

        // 🔥 PUTAWAY (Increase Inventory)

       

        [HttpPost("putaway")]

        [Authorize(Roles = "Admin,Operator")]

        public async Task<IActionResult> PutAway(CreatePutAwayTaskDTO dto)

        {

            var result = await _service.PutAwayAsync(dto);

            return Ok(result);

        }


        // 🔥 PICK (Reserve Inventory)



        [HttpPost("pick")]
        [Authorize(Roles = "Admin,Operator")]
        public async Task<IActionResult> Pick(PickTaskCreateDto dto)
        {
            var result = await _service.CreatePickAsync(dto);
            return Ok(result);
        }






    }

    }


