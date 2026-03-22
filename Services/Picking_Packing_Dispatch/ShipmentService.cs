using Microsoft.EntityFrameworkCore;
using WarehousePro.API.Models.Enums;
using WarehouseProject.Data;
using WarehouseProject.DTOs.Outbound;
using WarehouseProject.Models;
using WarehouseProject.Services;

public class ShipmentService : IShipmentService
{
    private readonly WarehouseDBContext _context;
    private readonly INotificationService _notificationService;

    public ShipmentService(WarehouseDBContext context, INotificationService notificationService)
    {
        _context = context;
        _notificationService = notificationService;
    }
    // ✅ CREATE SHIPMENT
    public async Task<ShipmentResponseDto> CreateAsync(ShipmentCreateDto dto)
    {
        // check order
        var order = await _context.Orders.FindAsync(dto.OrderID);
        if (order == null)
            throw new Exception("Order not found");
        // optional: check packing done
        var packed = await _context.PackingUnits
            .AnyAsync(x => x.OrderID == dto.OrderID);
        if (!packed)
            throw new Exception("Packing not completed");
        var shipment = new ShipmentModel
        {
            OrderID = dto.OrderID,
            Carrier = dto.Carrier,
            DispatchDate = DateTime.Now,
            Status = ShipmentStatus.Dispatched
        };
        _context.Shipments.Add(shipment);
        // update order
        order.Status = OrderStatus.Shipped;
        await _context.SaveChangesAsync();
        return new ShipmentResponseDto
        {
            ShipmentID = shipment.ShipmentID,
            OrderID = shipment.OrderID,
            Carrier = shipment.Carrier,
            DispatchDate = shipment.DispatchDate,
            DeliveryDate = shipment.DeliveryDate,
            Status = shipment.Status.ToString()
        };
    }
    // ✅ GET ALL
    public async Task<List<ShipmentResponseDto>> GetAllAsync()
    {
        return await _context.Shipments
            .Select(x => new ShipmentResponseDto
            {
                ShipmentID = x.ShipmentID,
                OrderID = x.OrderID,
                Carrier = x.Carrier,
                DispatchDate = x.DispatchDate,
                DeliveryDate = x.DeliveryDate,
                Status = x.Status.ToString()
            })
            .ToListAsync();
    }
    // ✅ UPDATE STATUS (DELIVERED)
    public async Task<ShipmentResponseDto?> UpdateAsync(int id, ShipmentUpdateDto dto)

    {

        var shipment = await _context.Shipments.FindAsync(id);

        if (shipment == null)

            return null;

        shipment.Status = dto.Status;

        // 🔥 WHEN DELIVERED

        if (dto.Status == ShipmentStatus.Delivered)

        {

            shipment.DeliveryDate = DateTime.Now;

            var order = await _context.Orders.FindAsync(shipment.OrderID);

            if (order != null)

            {

                order.Status = OrderStatus.Completed;

            }

            // ✅ 🔥 ADD NOTIFICATION HERE

            await _notificationService.CreateAsync(

                1, // userId (later from JWT)

                $"Order {shipment.OrderID} Delivered Successfully",

                NotificationCategory.Dispatch

            );

        }

        await _context.SaveChangesAsync();

        return new ShipmentResponseDto

        {

            ShipmentID = shipment.ShipmentID,

            OrderID = shipment.OrderID,

            Carrier = shipment.Carrier,

            DispatchDate = shipment.DispatchDate,

            DeliveryDate = shipment.DeliveryDate,

            Status = shipment.Status.ToString()

        };

    }

}
