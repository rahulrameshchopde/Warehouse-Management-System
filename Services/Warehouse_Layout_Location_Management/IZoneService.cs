using WarehouseProject.DTOs;
using WarehouseProject.Models;
namespace WarehouseProject.Services
{
    public interface IZoneService
    {
        Task<IEnumerable<ZoneModel>> GetAll();
        Task<ZoneModel> Create(ZoneDTO dto);
        Task<bool> Delete(int id);
    }
}