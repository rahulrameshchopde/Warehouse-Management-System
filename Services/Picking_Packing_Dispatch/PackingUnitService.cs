using Microsoft.EntityFrameworkCore;
using WarehousePro.API.Models.Enums;
using WarehouseProject.Data;
using WarehouseProject.DTOs.Outbound;
using WarehouseProject.Models;
using WarehouseProject.Services;

public class PackingService : IPackingUnitService

{

    private readonly WarehouseDBContext _context;
    private readonly INotificationService _notificationService;
    public PackingService(WarehouseDBContext context, INotificationService notificationService)
    {
        _context = context;
        _notificationService = notificationService;
    }

    // ✅ CREATE PACKING

    public async Task<PackingResponseDto> CreateAsync(PackingCreateDto dto)

    {

        // ✅ check order exists

        var order = await _context.Orders.FindAsync(dto.OrderID);

        if (order == null)

            throw new Exception("Order not found");

        // ✅ check all pick tasks completed

        var picks = await _context.PickTasks

            .Where(x => x.OrderID == dto.OrderID)

            .ToListAsync();

        if (!picks.Any())

            throw new Exception("No pick tasks found");

        if (picks.Any(x => x.Status != PickTaskStatus.Completed))

            throw new Exception("All pick tasks must be completed");

        // ✅ create packing

        var pack = new PackingUnitModel

        {

            OrderID = dto.OrderID,

            PackageType = dto.PackageType,

            Weight = dto.Weight,

            Status = PackingStatus.Packed

        };

        _context.PackingUnits.Add(pack);

        // ✅ update order status

        order.Status = OrderStatus.Processing;

        // 🔥 ✅ ADD NOTIFICATION HERE

        //await _notificationService.CreateAsync(

        //    1, // userId (static for now)

        //    $"Order {dto.OrderID} Packed Successfully",

        //    NotificationCategory.Picking

        //);

        await _context.SaveChangesAsync();

        return new PackingResponseDto

        {

            PackID = pack.PackID,

            OrderID = pack.OrderID,

            PackageType = pack.PackageType,

            Weight = pack.Weight,

            Status = pack.Status.ToString()

        };

    }


    // ✅ GET ALL

    public async Task<List<PackingResponseDto>> GetAllAsync()

    {

        return await _context.PackingUnits

            .Select(x => new PackingResponseDto

            {

                PackID = x.PackID,

                OrderID = x.OrderID,

                PackageType = x.PackageType,

                Weight = x.Weight,

                Status = x.Status.ToString()

            })

            .ToListAsync();

    }

    // ✅ UPDATE STATUS (SHIPPING FLOW)

    public async Task<bool> DeleteAsync(int id)

    {

        var pack = await _context.PackingUnits.FindAsync(id);

        if (pack == null)

            return false;

        _context.PackingUnits.Remove(pack);

        await _context.SaveChangesAsync();

        return true;

    }


    public async Task<PackingResponseDto?> UpdateAsync(int id, PackingStatusUpdateDto dto)

    {

        var pack = await _context.PackingUnits.FindAsync(id);

        if (pack == null)

            return null;

        pack.Status = dto.Status;

        // 🔥 If shipped → update order

        if (dto.Status == PackingStatus.Shipped)

        {

            var order = await _context.Orders.FindAsync(pack.OrderID);

            if (order != null)

            {

                order.Status = OrderStatus.Completed; // or Delivered

            }

        }




        await _context.SaveChangesAsync();

        return new PackingResponseDto

        {

            PackID = pack.PackID,

            OrderID = pack.OrderID,

            PackageType = pack.PackageType,

            Weight = pack.Weight,

            Status = pack.Status.ToString()

        };



    }

}
