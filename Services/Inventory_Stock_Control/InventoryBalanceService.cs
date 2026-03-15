using Microsoft.EntityFrameworkCore;
using WarehouseProject.Data;
using WarehouseProject.DTOs;
using WarehouseProject.Models;
namespace WarehouseProject.Services.Inventory_Stock_Control
{
    public class InventoryBalanceService : IInventoryBalanceService
    {
        private readonly WarehouseDBContext _context;
        public InventoryBalanceService(WarehouseDBContext context)
        {
            _context = context;
        }
        public async Task<InventoryBalanceModel> Create(InventoryBalanceDTO dto)
        {
            var inventory = new InventoryBalanceModel
            {
                ItemID = dto.ItemID,
                BinID = dto.BinID,
                QuantityOnHand = dto.QuantityOnHand,
                ReservedQuantity = dto.ReservedQuantity
            };
            _context.InventoryBalances.Add(inventory);
            await _context.SaveChangesAsync();
            return inventory;
        }
        public async Task<List<InventoryBalanceModel>> GetAll()
        {
            return await _context.InventoryBalances
                .Include(x => x.Item)
                .Include(x => x.BinLocation)
                .ToListAsync();
        }
        public async Task<InventoryBalanceModel> GetById(int id)
        {
            return await _context.InventoryBalances
                .Include(x => x.Item)
                .Include(x => x.BinLocation)
                .FirstOrDefaultAsync(x => x.BalanceID == id);
        }
        public async Task<bool> Delete(int id)
        {
            var inventory = await _context.InventoryBalances.FindAsync(id);
            if (inventory == null)
                return false;
            _context.InventoryBalances.Remove(inventory);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}