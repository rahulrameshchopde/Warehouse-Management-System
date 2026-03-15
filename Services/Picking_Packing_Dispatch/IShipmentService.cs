using WarehouseProject.Models;
using WarehouseProject.DTOs;
namespace WarehouseProject.Services.Picking_Packing_Dispatch
{
    public interface IShipmentService
    {
        Task<ShipmentModel> Create(ShipmentDTO dto);
        Task<List<ShipmentModel>> GetAll();
    }
}