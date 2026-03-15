using Microsoft.EntityFrameworkCore;
using WarehouseProject.Data;
using WarehouseProject.DTOs;
using WarehouseProject.Models;
namespace WarehouseProject.Services
{
    public class ZoneService : IZoneService
    {
        private readonly WarehouseDBContext _context;
        public ZoneService(WarehouseDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ZoneModel>> GetAll()
        {
            return await _context.Zones.ToListAsync();
        }
        public async Task<ZoneModel> Create(ZoneDTO dto)
        {
            var zone = new ZoneModel
            {
                WarehouseID = dto.WarehouseID,
                Name = dto.Name,
                ZoneType = dto.ZoneType
            };
            _context.Zones.Add(zone);
            await _context.SaveChangesAsync();
            return zone;
        }
        public async Task<bool> Delete(int id)
        {
            var zone = await _context.Zones.FindAsync(id);
            if (zone == null)
                return false;
            _context.Zones.Remove(zone);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}