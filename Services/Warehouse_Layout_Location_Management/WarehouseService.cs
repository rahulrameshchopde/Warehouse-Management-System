using WarehousePro.API.DTOs.Warehouse;
using WarehouseProject.Data;
using WarehouseProject.Models;
using Microsoft.EntityFrameworkCore;
public class WarehouseService : IWarehouseService

{

    private readonly WarehouseDBContext _context;

    public WarehouseService(WarehouseDBContext context)

    {

        _context = context;

    }

    public async Task<List<WarehouseResponseDto>> GetAllAsync()

    {

        return await _context.Warehouses

            .Include(w => w.Zones)

            .Select(w => MapToResponseDto(w))

            .ToListAsync();

    }

    public async Task<WarehouseResponseDto?> GetByIdAsync(int id)

    {

        var warehouse = await _context.Warehouses

            .Include(w => w.Zones)

            .FirstOrDefaultAsync(w => w.WarehouseID == id);

        return warehouse == null ? null : MapToResponseDto(warehouse);

    }

    public async Task<WarehouseResponseDto> CreateAsync(WarehouseCreateDto dto)

    {

        var warehouse = new WarehouseModel

        {

            Name = dto.Name,

            Location = dto.Location,


        };

        _context.Warehouses.Add(warehouse);

        await _context.SaveChangesAsync();

        return MapToResponseDto(warehouse);

    }

    public async Task<WarehouseResponseDto> UpdateAsync(int id, WarehouseUpdateDto dto)

    {

        var warehouse = await _context.Warehouses.FindAsync(id);

        if (warehouse == null)

            throw new Exception("Warehouse not found");

        warehouse.Name = dto.Name;

        warehouse.Location = dto.Location;

        warehouse.Status = dto.Status;

        await _context.SaveChangesAsync();

        return MapToResponseDto(warehouse);

    }

    public async Task<bool> DeleteAsync(int id)

    {

        var warehouse = await _context.Warehouses.FindAsync(id);

        if (warehouse == null)

            return false;

        _context.Warehouses.Remove(warehouse);

        await _context.SaveChangesAsync();

        return true;

    }

    private WarehouseResponseDto MapToResponseDto(WarehouseModel w)

    {

        return new WarehouseResponseDto

        {

            WarehouseID = w.WarehouseID,

            Name = w.Name,

            Location = w.Location,

            Status = w.Status.ToString(),

            CreatedAt = DateTime.UtcNow,

            TotalZones = w.Zones != null ? w.Zones.Count : 0

        };

    }

}
