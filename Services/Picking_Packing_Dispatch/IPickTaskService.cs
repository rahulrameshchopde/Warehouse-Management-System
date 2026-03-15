using WarehouseProject.DTOs;
using WarehouseProject.Models;
namespace WarehouseProject.Services.Picking_Packing_Dispatch
{
    public interface IPickTaskService
    {
        Task<PickTaskModel> Create(PickTaskDTO dto);
        Task<List<PickTaskModel>> GetAll();
        Task<PickTaskModel> GetById(int id);
        Task<bool> Delete(int id);
    }
}