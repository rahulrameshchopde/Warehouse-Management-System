using Microsoft.EntityFrameworkCore;
using WarehouseProject.Data;
using WarehouseProject.Models;
namespace WarehouseProject.Services
{
    public class WarehouseReportService : IWarehouseReportService
    {
        private readonly WarehouseDBContext _context;
        public WarehouseReportService(WarehouseDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<WarehouseReportModel>> GetAllAsync()
        {
            return await _context.WarehouseReports.ToListAsync();
        }
        public async Task<WarehouseReportModel> GetByIdAsync(int id)
        {
            return await _context.WarehouseReports.FindAsync(id);
        }
        public async Task<WarehouseReportModel> CreateAsync(WarehouseReportModel report)
        {
            report.GeneratedDate = DateTime.UtcNow;
            _context.WarehouseReports.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }
    }
}