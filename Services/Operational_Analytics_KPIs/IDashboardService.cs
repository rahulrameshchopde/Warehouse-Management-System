using WarehouseProject.DTOs.Notification;
namespace WarehouseProject.Services
{
    public interface IDashboardService
    {
        Task<DashboardDTO> GetDashboardData();
    }
}