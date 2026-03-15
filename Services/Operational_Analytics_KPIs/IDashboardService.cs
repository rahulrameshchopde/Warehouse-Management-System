using WarehouseProject.DTOs;
namespace WarehouseProject.Services
{
    public interface IDashboardService
    {
        Task<DashboardDTO> GetDashboardData();
    }
}