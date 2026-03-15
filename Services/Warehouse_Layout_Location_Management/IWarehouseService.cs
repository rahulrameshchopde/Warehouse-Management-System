using WarehouseProject.DTOs;
using WarehouseProject.Models;

namespace WarehouseProject.Services.Warehouse_Layout_Location_Management
{
    public interface IWarehouseService
    {
        Task<IEnumerable<WarehouseModel>> GetAll();
        Task<WarehouseModel> GetById(int id);
        Task<WarehouseModel> Create(WarehouseDTO dto);
        Task<bool> Delete(int id);
    }
}
