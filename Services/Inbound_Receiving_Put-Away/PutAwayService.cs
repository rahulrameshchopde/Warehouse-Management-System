using Microsoft.EntityFrameworkCore;

using WarehouseProject.DTOs;

using WarehouseProject.Models;

using WarehouseProject.Data;

namespace WarehouseProject.Services

{

    public class PutAwayService : IPutAwayService

    {

        private readonly WarehouseDBContext _context;

        public PutAwayService(WarehouseDBContext context)

        {

            _context = context;

        }

        // CREATE PUTAWAY TASK

        public async Task<PutAwayTaskModel> Create(PutAwayTaskDTO dto)

        {

            var task = new PutAwayTaskModel

            {

                ReceiptID = dto.ReceiptID,

                ItemID = dto.ItemID,

                TargetBinID = dto.TargetBinID,

                Quantity = dto.Quantity,

                Status = "Pending"

            };

            _context.PutAwayTasks.Add(task);

            await _context.SaveChangesAsync();

            // Inventory Update Logic

            var inventory = await _context.InventoryBalances

                .FirstOrDefaultAsync(x => x.ItemID == dto.ItemID && x.BinID == dto.TargetBinID);

            if (inventory == null)

            {

                inventory = new InventoryBalanceModel

                {

                    ItemID = dto.ItemID,

                    BinID = dto.TargetBinID,

                    QuantityOnHand = dto.Quantity,

                    ReservedQuantity = 0

                };

                _context.InventoryBalances.Add(inventory);

            }

            else

            {

                inventory.QuantityOnHand += dto.Quantity;

            }

            await _context.SaveChangesAsync();

            return await _context.PutAwayTasks

                .Include(x => x.Item)

                .Include(x => x.BinLocation)

                .Include(x => x.InboundReceipt)

                .FirstOrDefaultAsync(x => x.TaskID == task.TaskID);

        }


        // GET ALL PUTAWAY TASKS

        public async Task<List<PutAwayTaskModel>> GetAll()

        {

            return await _context.PutAwayTasks

                .Include(x => x.Item)

                .Include(x => x.BinLocation)

                .Include(x => x.InboundReceipt)

                .ToListAsync();

        }


        // COMPLETE TASK

        public async Task<PutAwayTaskModel> CompleteTask(int taskId)

        {

            var task = await _context.PutAwayTasks

                .FirstOrDefaultAsync(x => x.TaskID == taskId);

            if (task == null)

                return null;

            task.Status = "Completed";

            await _context.SaveChangesAsync();

            return task;

        }


        // DELETE TASK

        public async Task<bool> Delete(int taskId)

        {

            var task = await _context.PutAwayTasks

                .FirstOrDefaultAsync(x => x.TaskID == taskId);

            if (task == null)

                return false;

            _context.PutAwayTasks.Remove(task);

            await _context.SaveChangesAsync();

            return true;

        }

    }

}
