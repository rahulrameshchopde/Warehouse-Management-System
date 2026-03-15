using Microsoft.EntityFrameworkCore;

using WarehouseProject.Data;

using WarehouseProject.DTOs;

using WarehouseProject.Models;
using WarehouseProject.Services.Picking_Packing_Dispatch;

namespace WarehouseProject.Services

{

    public class PickTaskService : IPickTaskService

    {

        private readonly WarehouseDBContext _context;

        public PickTaskService(WarehouseDBContext context)

        {

            _context = context;

        }

        public async Task<PickTaskModel> Create(PickTaskDTO dto)

        {

            var task = new PickTaskModel

            {

                OrderID = dto.OrderID,

                ItemID = dto.ItemID,

                BinID = dto.BinID,

                PickQuantity = dto.PickQuantity,

                Status = dto.Status

            };

            _context.PickTasks.Add(task);

            await _context.SaveChangesAsync();

            return await _context.PickTasks

                .Include(x => x.Item)

                .Include(x => x.BinLocation)

                .FirstOrDefaultAsync(x => x.PickTaskID == task.PickTaskID);

        }

        public async Task<List<PickTaskModel>> GetAll()

        {

            return await _context.PickTasks

                .Include(x => x.Item)

                .Include(x => x.BinLocation)

                .ToListAsync();

        }

        public async Task<PickTaskModel> GetById(int id)

        {

            return await _context.PickTasks

                .Include(x => x.Item)

                .Include(x => x.BinLocation)

                .FirstOrDefaultAsync(x => x.PickTaskID == id);

        }

        public async Task<bool> Delete(int id)

        {

            var task = await _context.PickTasks.FindAsync(id);

            if (task == null)

                return false;

            _context.PickTasks.Remove(task);

            await _context.SaveChangesAsync();

            return true;

        }

    }

}
