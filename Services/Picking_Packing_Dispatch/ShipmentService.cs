using Microsoft.EntityFrameworkCore;
using WarehouseProject.Data;
using WarehouseProject.Models;
using WarehouseProject.DTOs;
namespace WarehouseProject.Services.Picking_Packing_Dispatch
{
    public class ShipmentService : IShipmentService
    {
        private readonly WarehouseDBContext _context;
        public ShipmentService(WarehouseDBContext context)
        {
            _context = context;
        }
        public async Task<ShipmentModel> Create(ShipmentDTO dto)
        {
            var shipment = new ShipmentModel
            {
                OrderID = dto.OrderID,
                Carrier = dto.Carrier,
                DispatchDate = DateTime.UtcNow,
                DeliveryDate = dto.DeliveryDate,
                Status = "Dispatched"
            };
            _context.Shipments.Add(shipment);
            await _context.SaveChangesAsync();
            return shipment;
        }
        public async Task<List<ShipmentModel>> GetAll()
        {
            return await _context.Shipments.ToListAsync();
        }
    }
}