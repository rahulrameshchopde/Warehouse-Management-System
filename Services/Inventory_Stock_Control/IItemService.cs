using WarehouseProject.DTOs;
using WarehouseProject.Models;
namespace WarehouseProject.Services.Inventory_Stock_Control
{
    public interface IItemService
    {
        Task<List<ItemModel>> GetAll();
        Task<ItemModel> GetById(int id);
        Task<ItemModel> Create(ItemDTO dto);
        Task<ItemModel> Update(int id, ItemDTO dto);
        Task<bool> Delete(int id);
    }
}