using WarehouseProject.DTOs;
using WarehouseProject.Models;
namespace WarehouseProject.Services.Inventory_Stock_Control
{
    public interface IInventoryBalanceService
    {
        Task<InventoryBalanceModel> Create(InventoryBalanceDTO dto);
        Task<List<InventoryBalanceModel>> GetAll();
        Task<InventoryBalanceModel> GetById(int id);
        Task<bool> Delete(int id);
    }
}