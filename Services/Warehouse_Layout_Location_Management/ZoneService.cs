using WarehousePro.API.DTOs.Zone;
using WarehouseProject.Data;
using WarehouseProject.Models;
using Microsoft.EntityFrameworkCore;

public class ZoneService : IZoneService
{
    private readonly WarehouseDBContext _context;
    public ZoneService(WarehouseDBContext context)
    {
        _context = context;
    }
    // all methods already given earlier


// 🔹 GET ALL

public async Task<List<ZoneResponseDto>> GetAllAsync()

    {

        return await _context.Zones

            .Include(z => z.Warehouse)

            .Select(z => new ZoneResponseDto

            {

                ZoneID = z.ZoneID,

                WarehouseID = z.WarehouseID,

                WarehouseName = z.Warehouse.Name,

                Name = z.Name,

                ZoneType = z.ZoneType.ToString()

            })

            .ToListAsync();

    }

    // 🔹 GET BY ID

    public async Task<ZoneResponseDto?> GetByIdAsync(int id)

    {

        var z = await _context.Zones

            .Include(x => x.Warehouse)

            .FirstOrDefaultAsync(x => x.ZoneID == id);

        if (z == null) return null;

        return new ZoneResponseDto

        {

            ZoneID = z.ZoneID,

            WarehouseID = z.WarehouseID,

            WarehouseName = z.Warehouse.Name,

            Name = z.Name,

            ZoneType = z.ZoneType.ToString()

        };

    }

    // 🔹 CREATE

    public async Task<ZoneResponseDto> CreateAsync(ZoneCreateDto dto)

    {

        var zone = new ZoneModel

        {

            WarehouseID = dto.WarehouseID,

            Name = dto.Name,

            

            ZoneType = dto.ZoneType

        };

        _context.Zones.Add(zone);

        await _context.SaveChangesAsync();

        return await GetByIdAsync(zone.ZoneID);

    }

    // 🔹 UPDATE

    public async Task<bool> UpdateAsync(int id, ZoneUpdateDto dto)

    {

        var zone = await _context.Zones.FindAsync(id);

        if (zone == null) return false;

        zone.Name = dto.Name;

        zone.ZoneType = dto.ZoneType;

        await _context.SaveChangesAsync();

        return true;

    }

    // 🔹 DELETE

    public async Task<bool> DeleteAsync(int id)

    {

        var zone = await _context.Zones.FindAsync(id);

        if (zone == null) return false;

        _context.Zones.Remove(zone);

        await _context.SaveChangesAsync();

        return true;

    }

}
