using Microsoft.EntityFrameworkCore;
using WarehouseProject.Data;
using WarehouseProject.DTOs;
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

        public async Task<IEnumerable<BinLocationModel>> GetAll()

        {

            return await _context.BinLocations.ToListAsync();

        }

        public async Task<BinLocationModel> Create(BinLocationDTO dto)

        {

            var bin = new BinLocationModel

            {

                ZoneID = dto.ZoneID,

                Code = dto.Code,

                Capacity = dto.Capacity,

                Status = dto.Status

            };

            _context.BinLocations.Add(bin);

            await _context.SaveChangesAsync();

            return bin;

        }

        public async Task<bool> Delete(int id)

        {

            var bin = await _context.BinLocations.FindAsync(id);

            if (bin == null)

                return false;

            _context.BinLocations.Remove(bin);

            await _context.SaveChangesAsync();

            return true;

        }

    }

}
