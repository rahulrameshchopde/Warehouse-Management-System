using Microsoft.EntityFrameworkCore;
using WarehouseProject.Data;
using WarehouseProject.Models;
using WarehouseProject.DTOs;
namespace WarehouseProject.Services
{
    public class PackingUnitService : IPackingUnitService
    {
        private readonly WarehouseDBContext _context;
        public PackingUnitService(WarehouseDBContext context)
        {
            _context = context;
        }
        public async Task<PackingUnitModel> Create(PackingUnitDTO dto)
        {
            var pack = new PackingUnitModel
            {
                OrderID = dto.OrderID,
                PackageType = dto.PackageType,
                Weight = dto.Weight,
                Status = "Packed"
            };
            _context.PackingUnits.Add(pack);
            await _context.SaveChangesAsync();
            return pack;
        }
        public async Task<List<PackingUnitModel>> GetAll()
        {
            return await _context.PackingUnits.ToListAsync();
        }
        public async Task<PackingUnitModel> CompletePacking(int packId)
        {
            var pack = await _context.PackingUnits
                .FirstOrDefaultAsync(x => x.PackID == packId);
            if (pack == null)
                return null;
            pack.Status = "Completed";
            await _context.SaveChangesAsync();
            return pack;
        }
    }
}