using Microsoft.EntityFrameworkCore;
using WarehouseProject.Data;
using WarehouseProject.DTOs;
namespace WarehouseProject.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly WarehouseDBContext _context;
        public DashboardService(WarehouseDBContext context)
        {
            _context = context;
        }
        public async Task<DashboardDTO> GetDashboardData()
        {
            var dashboard = new DashboardDTO
            {
                TotalItems = await _context.Items.CountAsync(),
                TotalWarehouses = await _context.Warehouses.CountAsync(),
                TotalZones = await _context.Zones.CountAsync(),
                TotalBins = await _context.BinLocations.CountAsync(),
                TotalPickTasks = await _context.PickTasks.CountAsync(),
                TotalShipments = await _context.Shipments.CountAsync(),
                TotalReplenishments = await _context.ReplenishmentTasks.CountAsync()
            };
            return dashboard;
        }
    }
}