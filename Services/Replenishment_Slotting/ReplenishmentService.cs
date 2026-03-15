using Microsoft.EntityFrameworkCore;
using WarehouseProject.Data;
using WarehouseProject.DTOs;
using WarehouseProject.Models;
namespace WarehouseProject.Services.Replenishment_Slotting
{
    public class ReplenishmentService : IReplenishmentService
    {
        private readonly WarehouseDBContext _context;
        public ReplenishmentService(WarehouseDBContext context)
        {
            _context = context;
        }
        public async Task<ReplenishmentTaskModel> Create(ReplenishmentDTO dto)
        {
            var task = new ReplenishmentTaskModel
            {
                ItemID = dto.ItemID,
                FromBinID = dto.FromBinID,
                ToBinID = dto.ToBinID,
                Quantity = dto.Quantity,
                Status = "Pending"
            };
            _context.ReplenishmentTasks.Add(task);
            await _context.SaveChangesAsync();
            return await _context.ReplenishmentTasks
                .Include(x => x.Item)
                .Include(x => x.FromBin)
                .Include(x => x.ToBin)
                .FirstOrDefaultAsync(x => x.ReplenishID == task.ReplenishID);
        }
        public async Task<List<ReplenishmentTaskModel>> GetAll()
        {
            return await _context.ReplenishmentTasks
                .Include(x => x.Item)
                .Include(x => x.FromBin)
                .Include(x => x.ToBin)
                .ToListAsync();
        }
        public async Task<ReplenishmentTaskModel> CompleteTask(int id)
        {
            var task = await _context.ReplenishmentTasks
                .FirstOrDefaultAsync(x => x.ReplenishID == id);
            if (task == null)
                return null;
            task.Status = "Completed";
            await _context.SaveChangesAsync();
            return task;
        }
    }
}