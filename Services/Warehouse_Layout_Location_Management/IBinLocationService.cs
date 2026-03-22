using WarehouseProject.DTOs.BinLocationDTOs;
using WarehouseProject.Models;
namespace WarehouseProject.Services
{
    public interface IBinLocationService
    {
        Task<IEnumerable<BinLocationResponseDTO>> GetAll();
        Task<BinLocationResponseDTO> GetById(int id);
        Task<BinLocationResponseDTO> Create(BinLocationModel bin);
        Task<BinLocationResponseDTO> Update(int id, BinLocationModel bin);
        Task<bool> Delete(int id);
    }
}