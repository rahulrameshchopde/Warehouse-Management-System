using Microsoft.EntityFrameworkCore;
using WarehouseProject.Data;
using WarehouseProject.DTOs;
using WarehouseProject.DTOs.itemDtos;
using WarehouseProject.Models;
public class ItemService : IItemService
{
    private readonly WarehouseDBContext _context;
    public ItemService(WarehouseDBContext context)
    {
        _context = context;
    }
    // ✅ GET ALL
    public async Task<IEnumerable<ItemResponseDTO>> GetAll()
    {
        return await _context.Items
            .Select(i => new ItemResponseDTO
            {
                ItemID = i.ItemID,
                Name = i.Name,
                SKU = i.SKU,
                Description = i.Description,
                UnitOfMeasure = i.UnitOfMeasure,
               
            }).ToListAsync();
    }
    // ✅ GET BY ID
    public async Task<ItemResponseDTO> GetById(int id)
    {
        return await _context.Items
            .Where(i => i.ItemID == id)
            .Select(i => new ItemResponseDTO
            {
                ItemID = i.ItemID,
                Name = i.Name,
                SKU = i.SKU,
                Description = i.Description,
                UnitOfMeasure = i.UnitOfMeasure,
               
            }).FirstOrDefaultAsync();
    }
    // ✅ CREATE (Duplicate check)
    public async Task<ItemResponseDTO> Create(CreateItemDTO dto)
    {
        var exists = await _context.Items
            .AnyAsync(x => x.SKU.ToLower().Trim() == dto.SKU.ToLower().Trim());
        if (exists) return null;
        var item = new ItemModel
        {
            Name = dto.Name,
            SKU = dto.SKU,
            Description = dto.Description,
            UnitOfMeasure = dto.UnitOfMeasure,
            
        };
        _context.Items.Add(item);
        await _context.SaveChangesAsync();
        return new ItemResponseDTO
        {
            ItemID = item.ItemID,
            Name = item.Name,
            SKU = item.SKU,
            Description = item.Description,
            UnitOfMeasure = item.UnitOfMeasure,
            Status = item.Status.ToString()
           
        };
    }
    // ✅ UPDATE (Duplicate check)
    public async Task<ItemResponseDTO> Update(int id, UpdateItemDTO dto)
    {
        var existing = await _context.Items.FindAsync(id);
        if (existing == null) return null;
        var duplicate = await _context.Items
            .AnyAsync(x => x.SKU.ToLower().Trim() == dto.SKU.ToLower().Trim() && x.ItemID != id);
        if (duplicate) return null;
        existing.Name = dto.Name;
        existing.SKU = dto.SKU;
        existing.Description = dto.Description;
        existing.UnitOfMeasure = dto.UnitOfMeasure;
        await _context.SaveChangesAsync();
        return new ItemResponseDTO
        {
            ItemID = existing.ItemID,
            Name = existing.Name,
            SKU = existing.SKU,
            Description = existing.Description,
            UnitOfMeasure = existing.UnitOfMeasure,
            
        };
    }
    // ✅ DELETE
    public async Task<bool> Delete(int id)
    {
        var item = await _context.Items.FindAsync(id);
        if (item == null) return false;
        _context.Items.Remove(item);
        await _context.SaveChangesAsync();
        return true;
    }
}