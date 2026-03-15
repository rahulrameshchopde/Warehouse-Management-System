using WarehouseProject.DTOs;
using WarehouseProject.Models;
namespace WarehouseProject.Services
{
    public interface IBinLocationService
    {
        Task<IEnumerable<BinLocationModel>> GetAll();
        Task<BinLocationModel> Create(BinLocationDTO dto);
        Task<bool> Delete(int id);
    }
}