using Microsoft.EntityFrameworkCore;
using WarehousePro.API.DTOs.Outbound;
using WarehouseProject.Data;
using WarehouseProject.DTOs;
using WarehouseProject.DTOs.InventoryBalanceDTOs;
using WarehouseProject.DTOs.Outbound;
using WarehouseProject.DTOs.PutAwayTaskDTOs;
using WarehouseProject.Models;
public class InventoryBalanceService : IInventoryBalanceService
{
    private readonly WarehouseDBContext _context;
    public InventoryBalanceService(WarehouseDBContext context)
    {
        _context = context;
    }
   
    // ✅ GET ALL
   
    public async Task<IEnumerable<InventoryBalanceResponseDTO>> GetAll()
    {
        return await _context.InventoryBalances

            .Include(x => x.Item)           // ✅ ADD THIS
            .Include(x => x.BinLocation)    // ✅ ADD THIS
            .Select(x => new InventoryBalanceResponseDTO
    {

             BalanceID = x.BalanceID,
             ItemID = x.ItemID,
             BinID = x.BinID,
             ItemName = x.Item.Name,          // ✅ correct
             BinCode = x.BinLocation.Code,    // ✅ correct
             QuantityOnHand = x.QuantityOnHand,
             ReservedQuantity = x.ReservedQuantity

    })

    .ToListAsync();

    }

    // ✅ GET BY ID

    public async Task<InventoryBalanceResponseDTO> GetById(int id)
    {
        return await _context.InventoryBalances
            .Where(x => x.BalanceID == id)
            .Select(x => new InventoryBalanceResponseDTO
            {
                BalanceID = x.BalanceID,
                ItemID = x.ItemID,
                BinID = x.BinID,
                QuantityOnHand = x.QuantityOnHand,
                ReservedQuantity = x.ReservedQuantity
            }).FirstOrDefaultAsync();
    }
    
    // ✅ CREATE
    
    public async Task<InventoryBalanceResponseDTO> Create(CreateInventoryBalanceDTO dto)
    {
        var item = await _context.Items.FindAsync(dto.ItemID);
        var bin = await _context.BinLocations.FindAsync(dto.BinID);
        if (item == null || bin == null)
            return null;
        var exists = await _context.InventoryBalances
            .AnyAsync(x => x.ItemID == dto.ItemID && x.BinID == dto.BinID);
        if (exists)
            return null;
        var entity = new InventoryBalanceModel
        {
            ItemID = dto.ItemID,
            BinID = dto.BinID,
            QuantityOnHand = dto.QuantityOnHand,
            ReservedQuantity = dto.ReservedQuantity
        };
        _context.InventoryBalances.Add(entity);
        await _context.SaveChangesAsync();
        return new InventoryBalanceResponseDTO
        {
            BalanceID = entity.BalanceID,
            ItemID = entity.ItemID,
            BinID = entity.BinID,
            QuantityOnHand = entity.QuantityOnHand,
            ReservedQuantity = entity.ReservedQuantity
        };
    }
   
    // ✅ UPDATE
    
    public async Task<InventoryBalanceResponseDTO> Update(int id, UpdateInventoryBalanceDTO dto)
    {
        var existing = await _context.InventoryBalances.FindAsync(id);
        if (existing == null)
            return null;
        existing.QuantityOnHand = dto.QuantityOnHand;
        existing.ReservedQuantity = dto.ReservedQuantity;
        await _context.SaveChangesAsync();
        return new InventoryBalanceResponseDTO
        {
            BalanceID = existing.BalanceID,
            ItemID = existing.ItemID,
            BinID = existing.BinID,
            QuantityOnHand = existing.QuantityOnHand,
            ReservedQuantity = existing.ReservedQuantity
        };
    }
    
    // ✅ DELETE
  
    public async Task<bool> Delete(int id)
    {
        var existing = await _context.InventoryBalances.FindAsync(id);
        if (existing == null)
            return false;
        _context.InventoryBalances.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
    
    // 🔥 PUTAWAY (Increase Inventory)
   
    public async Task<string> PutAwayAsync(CreatePutAwayTaskDTO dto)
    {
        var inventory = await _context.InventoryBalances
            .FirstOrDefaultAsync(x =>
                x.ItemID == dto.ItemID &&
                x.BinID == dto.TargetBinID);
        if (inventory != null)
        {
            inventory.QuantityOnHand += dto.Quantity;
        }
        else
        {
            _context.InventoryBalances.Add(new InventoryBalanceModel
            {
                ItemID = dto.ItemID,
                BinID = dto.TargetBinID,
                QuantityOnHand = dto.Quantity,
                ReservedQuantity = 0
            });
        }
        await _context.SaveChangesAsync();
        return "PutAway Done";
    }
    
    // 🔥 PICK (Reserve Inventory)
   
    public async Task<string> CreatePickAsync(PickTaskCreateDto dto)
    {
        var inventory = await _context.InventoryBalances
            .FirstOrDefaultAsync(x =>
                x.ItemID == dto.ItemID &&
                x.BinID == dto.BinID);
        if (inventory == null)
            return "Inventory not found";
        int available = inventory.QuantityOnHand - inventory.ReservedQuantity;
        if (available < dto.PickQuantity)
            return "Not enough stock";
        inventory.ReservedQuantity += dto.PickQuantity;
        _context.PickTasks.Add(new PickTaskModel
        {
            OrderID = dto.OrderID,
            ItemID = dto.ItemID,
            BinID = dto.BinID,
            PickQuantity = dto.PickQuantity,
          
        });
        await _context.SaveChangesAsync();
        return "Stock Reserved";
    }
    
    // 🔥 CONFIRM PICK (Final Deduction)
  
   
}