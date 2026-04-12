using Microsoft.EntityFrameworkCore;
using WarehousePro.API.Models.Enums;
using WarehouseProject.Data;
using WarehouseProject.DTOs.BinLocationDTOs;
using WarehouseProject.Models;
namespace WarehouseProject.Services
{
    public class BinLocationService : IBinLocationService
    {
        private readonly WarehouseDBContext _context;
        public BinLocationService(WarehouseDBContext context)
        {
            _context = context;
        }
        // ✅ GET ALL
        public async Task<IEnumerable<BinLocationResponseDTO>> GetAll()
        {
            var data = await _context.BinLocations.ToListAsync();
            return data.Select(b => new BinLocationResponseDTO
            {
                BinID = b.BinID,
                ZoneID = b.ZoneID,
                Code = b.Code,
                Capacity = b.Capacity,
                Status = b.Status.ToString()

            });
        }
        // ✅ GET BY ID
        public async Task<BinLocationResponseDTO> GetById(int id)
        {
            var b = await _context.BinLocations.FindAsync(id);
            if (b == null) return null;
            return new BinLocationResponseDTO
            {
                BinID = b.BinID,
                ZoneID = b.ZoneID,
                Code = b.Code,
                Capacity = b.Capacity,
                Status = b.Status.ToString()

            };
        }
        // ✅ CREATE
        public async Task<BinLocationResponseDTO> Create(BinLocationModel model)
        {
            _context.BinLocations.Add(model);
            await _context.SaveChangesAsync();
            return new BinLocationResponseDTO
            {
                BinID = model.BinID,
                ZoneID = model.ZoneID,
                Code = model.Code,
                Capacity = model.Capacity,
                Status = model.Status.ToString()
            };
        }
        // ✅ UPDATE
        public async Task<BinLocationResponseDTO> Update(int id, BinLocationModel model)
        {
            var existing = await _context.BinLocations.FindAsync(id);
            if (existing == null) return null;
            existing.ZoneID = model.ZoneID;
            existing.Code = model.Code;
            existing.Capacity = model.Capacity;
            existing.Status = model.Status;
            await _context.SaveChangesAsync();
            return new BinLocationResponseDTO
            {
                BinID = existing.BinID,
                ZoneID = existing.ZoneID,
                Code = existing.Code,
                Capacity = existing.Capacity,
                
            };
        }
        // ✅ DELETE
        public async Task<bool> Delete(int id)
        {
            var existing = await _context.BinLocations.FindAsync(id);
            if (existing == null) return false;
            _context.BinLocations.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}