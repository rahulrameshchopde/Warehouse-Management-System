using Microsoft.EntityFrameworkCore;
using WarehousePro.API.DTOs.Outbound;
using WarehousePro.API.Models;
using WarehousePro.API.Models.Enums;
using WarehousePro.API.Services.Interfaces;
using WarehouseProject.Data;
using WarehouseProject.Models;
using WarehouseProject.Services;


namespace WarehousePro.API.Services
{
   
public class PickTaskService : IPickTaskService

    {

        private readonly WarehouseDBContext _context;

        private readonly INotificationService _notificationService;

        public PickTaskService(WarehouseDBContext context, INotificationService notificationService)

        {

            _context = context;

            _notificationService = notificationService;

        }


        // 🔥 1. CREATE PICK (RESERVE STOCK)

        public async Task<PickTaskResponseDto> CreatePickAsync(PickTaskCreateDto dto)

        {

            var order = await _context.Orders.FindAsync(dto.OrderID)

                ?? throw new Exception("Order not found");

            var item = await _context.Items.FindAsync(dto.ItemID)

                ?? throw new Exception("Item not found");

            var bin = await _context.BinLocations.FindAsync(dto.BinID)

                ?? throw new Exception("Bin not found");

            var inventory = await _context.InventoryBalances

                .FirstOrDefaultAsync(x => x.ItemID == dto.ItemID && x.BinID == dto.BinID);

            if (inventory == null)

                throw new Exception("Inventory not found");

            if (inventory.QuantityOnHand < dto.PickQuantity)

                throw new Exception("Not enough stock");

            // ✅ RESERVE STOCK

            inventory.ReservedQuantity += dto.PickQuantity;

            var pick = new PickTaskModel

            {

                OrderID = dto.OrderID,

                ItemID = dto.ItemID,

                BinID = dto.BinID,

                PickQuantity = dto.PickQuantity,

                Status = PickTaskStatus.Created

            };

            _context.PickTasks.Add(pick);

            // update order

            order.Status = OrderStatus.Processing;

            await _context.SaveChangesAsync();

            return await MapToDto(pick.PickTaskID);

        }

        // 🔥 2. AUTO CREATE FROM ORDER

        public async Task AutoCreateFromOrder(int orderId)

        {

            var order = await _context.Orders.FindAsync(orderId)

                ?? throw new Exception("Order not found");

            var exists = _context.PickTasks.Any(x => x.OrderID == orderId);

            if (exists)

                throw new Exception("PickTasks already created");

            var orderItems = await _context.OrderItems

                .Where(x => x.OrderID == orderId)

                .ToListAsync();

            foreach (var item in orderItems)

            {

                var stock = await _context.InventoryBalances

                    .FirstOrDefaultAsync(x => x.ItemID == item.ItemID);

                if (stock == null)

                    throw new Exception($"No stock for ItemID {item.ItemID}");

                if (stock.QuantityOnHand < item.Quantity)

                    throw new Exception("Not enough stock");

                // ✅ RESERVE

                stock.ReservedQuantity += item.Quantity;

                var pick = new PickTaskModel

                {

                    OrderID = orderId,

                    ItemID = item.ItemID,

                    BinID = stock.BinID,

                    PickQuantity = item.Quantity,

                    Status = PickTaskStatus.Created

                };

                _context.PickTasks.Add(pick);

            }

            order.Status = OrderStatus.Processing;

            await _context.SaveChangesAsync();

        }

        // 🔥 3. GET ALL

        public async Task<List<PickTaskResponseDto>> GetAllAsync()

        {

            return await _context.PickTasks

                .Include(x => x.Order)

                .Include(x => x.Item)

                .Include(x => x.BinLocation)

                .Select(x => new PickTaskResponseDto

                {

                    PickTaskID = x.PickTaskID,

                    OrderID = x.OrderID,

                    OrderNumber = x.Order.OrderNumber,

                    ItemID = x.ItemID,

                    ItemName = x.Item.Name,

                    BinID = x.BinID,

                    BinCode = x.BinLocation.Code,

                    PickQuantity = x.PickQuantity,

                    Status = x.Status.ToString()

                })

                .ToListAsync();

        }

        // 🔥 4. UPDATE STATUS (FINAL STOCK REDUCE)

        public async Task<PickTaskResponseDto?> UpdateStatusAsync(int id, PickTaskUpdateDto dto)

        {

            var pick = await _context.PickTasks.FindAsync(id);

            if (pick == null) return null;

            var oldStatus = pick.Status;

            pick.Status = dto.Status;

            // ✅ FIXED CONDITION

            if (oldStatus != PickTaskStatus.Completed &&

                dto.Status == PickTaskStatus.Completed)

            {

                var inventory = await _context.InventoryBalances

                    .FirstOrDefaultAsync(x =>

                        x.ItemID == pick.ItemID &&

                        x.BinID == pick.BinID);

                if (inventory == null)

                    throw new Exception("Inventory not found");

                if (inventory.ReservedQuantity < pick.PickQuantity)

                    throw new Exception("Reserved quantity issue");

                // ✅ update stock

                inventory.ReservedQuantity -= pick.PickQuantity;

                inventory.QuantityOnHand -= pick.PickQuantity;

                // 🔥 ✅ ADD NOTIFICATION HERE

                await _notificationService.CreateAsync(

                    1,

                    $"Order {pick.OrderID} Picking Completed",

                    NotificationCategory.Picking

                );

            }

            await _context.SaveChangesAsync();

            return await MapToDto(id);

        }


        // 🔥 PRIVATE MAPPER

        private async Task<PickTaskResponseDto> MapToDto(int id)

        {

            return await _context.PickTasks

                .Include(x => x.Order)

                .Include(x => x.Item)

                .Include(x => x.BinLocation)

                .Where(x => x.PickTaskID == id)

                .Select(x => new PickTaskResponseDto

                {

                    PickTaskID = x.PickTaskID,

                    OrderID = x.OrderID,

                    OrderNumber = x.Order.OrderNumber,

                    ItemID = x.ItemID,

                    ItemName = x.Item.Name,

                    BinID = x.BinID,

                    BinCode = x.BinLocation.Code,

                    PickQuantity = x.PickQuantity,

                    Status = x.Status.ToString()

                })

                .FirstAsync();

        }

    } }
 