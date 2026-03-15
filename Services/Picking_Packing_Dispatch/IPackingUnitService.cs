using WarehouseProject.Models;
using WarehouseProject.DTOs;
namespace WarehouseProject.Services
{
    public interface IPackingUnitService
    {
        Task<PackingUnitModel> Create(PackingUnitDTO dto);
        Task<List<PackingUnitModel>> GetAll();
        Task<PackingUnitModel> CompletePacking(int packId);
    }
}