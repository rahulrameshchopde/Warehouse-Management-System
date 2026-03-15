using WarehouseProject.Models;
namespace WarehouseProject.Services
{
    public interface IWarehouseReportService
    {
        Task<IEnumerable<WarehouseReportModel>> GetAllAsync();
        Task<WarehouseReportModel> GetByIdAsync(int id);
        Task<WarehouseReportModel> CreateAsync(WarehouseReportModel report);
    }
}