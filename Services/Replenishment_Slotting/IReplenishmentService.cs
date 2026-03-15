using WarehouseProject.DTOs;
using WarehouseProject.Models;
namespace WarehouseProject.Services.Replenishment_Slotting
{
    public interface IReplenishmentService
    {
        Task<ReplenishmentTaskModel> Create(ReplenishmentDTO dto);
        Task<List<ReplenishmentTaskModel>> GetAll();
        Task<ReplenishmentTaskModel> CompleteTask(int id);
    }
}