using WarehousePro.API.Models.Enums;
using WarehouseProject.Data;
using WarehouseProject.DTOs.Order;
using WarehouseProject.Models;
using Microsoft.EntityFrameworkCore;
using WarehouseProject.Services.Order;

public class OrderService : IOrderService

{

    private readonly WarehouseDBContext _context;

    public OrderService(WarehouseDBContext context)

    {

        _context = context;

    }

    public async Task<OrderResponseDto> CreateAsync(OrderCreateDto dto)

    {

        var order = new OrderModel

        {

            OrderNumber = dto.OrderNumber,

            CustomerName = dto.CustomerName,

            DeliveryAddress = dto.DeliveryAddress,

            OrderDate = dto.OrderDate,

            RequiredDate = dto.RequiredDate,

            Status = OrderStatus.Created,

            OrderItems = dto.Items.Select(i => new OrderItemModel

            {

                ItemID = i.ItemID,

                Quantity = i.Quantity

            }).ToList()

        };

        _context.Orders.Add(order);

        await _context.SaveChangesAsync();

        return Map(order);

    }

    public async Task<List<OrderResponseDto>> GetAllAsync()

    {

        return await _context.Orders

            .Select(o => Map(o))

            .ToListAsync();

    }

    public async Task<OrderResponseDto?> GetByIdAsync(int id)

    {

        var order = await _context.Orders.FindAsync(id);

        return order == null ? null : Map(order);

    }

    public async Task<bool> UpdateAsync(int id, OrderUpdateDto dto)

    {

        var order = await _context.Orders.FindAsync(id);

        if (order == null) return false;

        order.DeliveryAddress = dto.DeliveryAddress ?? order.DeliveryAddress;

        order.RequiredDate = dto.RequiredDate ?? order.RequiredDate;

        await _context.SaveChangesAsync();

        return true;

    }

    public async Task<bool> DeleteAsync(int id)

    {

        var order = await _context.Orders.FindAsync(id);

        if (order == null) return false;

        _context.Orders.Remove(order);

        await _context.SaveChangesAsync();

        return true;

    }

    // 🔥 FLOW METHODS

    public async Task<bool> StartPicking(int id)

    {

        var order = await _context.Orders.FindAsync(id);

        if (order == null) return false;

        order.Status = OrderStatus.Picking;

        await _context.SaveChangesAsync();

        return true;

    }

    public async Task<bool> CompletePicking(int id)

    {

        var order = await _context.Orders.FindAsync(id);

        if (order == null) return false;

        order.Status = OrderStatus.Packed;

        await _context.SaveChangesAsync();

        return true;

    }

    public async Task<bool> Ship(int id)

    {

        var order = await _context.Orders.FindAsync(id);

        if (order == null) return false;

        order.Status = OrderStatus.Shipped;

        await _context.SaveChangesAsync();

        return true;

    }

    public async Task<bool> Deliver(int id)

    {

        var order = await _context.Orders.FindAsync(id);

        if (order == null) return false;

        order.Status = OrderStatus.Delivered;

        await _context.SaveChangesAsync();

        return true;

    }

    // 🔁 MAPPER

    private static OrderResponseDto Map(OrderModel o)

    {

        return new OrderResponseDto

        {

            OrderID = o.OrderID,

            OrderNumber = o.OrderNumber,

            CustomerName = o.CustomerName,

            DeliveryAddress = o.DeliveryAddress,

            OrderDate = o.OrderDate,

            RequiredDate = o.RequiredDate,

            Status = o.Status.ToString()

        };

    }

}
