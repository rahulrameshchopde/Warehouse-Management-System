using Microsoft.EntityFrameworkCore;
using WarehousePro.API.Models.Enums;
using WarehouseProject.Data;
using WarehouseProject.DTOs.PutAwayTaskDTOs;
using WarehouseProject.Models;

public class PutAwayTaskService : IPutAwayTaskService

{

    private readonly WarehouseDBContext _context;
    public PutAwayTaskService(WarehouseDBContext context)

    {
        _context = context;
    }

    // ✅ GET ALL

    public async Task<IEnumerable<PutAwayTaskResponseDTO>> GetAll()

    {

        return await _context.PutAwayTasks
            .Select(x => new PutAwayTaskResponseDTO

            {

                TaskID = x.TaskID,
                ReceiptID = x.ReceiptID,
                ItemID = x.ItemID,
                TargetBinID = x.TargetBinID,
                Quantity = x.Quantity,
                Status = x.Status.ToString()
            }).ToListAsync();
    }

    // ✅ GET BY ID
    public async Task<PutAwayTaskResponseDTO> GetById(int id)

    {
        var data = await _context.PutAwayTasks
            .Where(x => x.TaskID == id)
            .Select(x => new PutAwayTaskResponseDTO

            {

                TaskID = x.TaskID,
                ReceiptID = x.ReceiptID,
                ItemID = x.ItemID,
                TargetBinID = x.TargetBinID,
                Quantity = x.Quantity,
                Status = x.Status.ToString()

            }).FirstOrDefaultAsync();

        return data;

    }

    // 🔥 MAIN METHOD (CREATE + UPDATE + INVENTORY ALL IN ONE)

    public async Task<string> PutAwayAsync(CreatePutAwayTaskDTO dto)

    {
        // ✅ FK VALIDATION
        var receipt = await _context.InboundReceipts.FindAsync(dto.ReceiptID);
        var item = await _context.Items.FindAsync(dto.ItemID);
        var bin = await _context.BinLocations.FindAsync(dto.TargetBinID);

        if (receipt == null || item == null || bin == null)
            return "Invalid FK Data ❌";

        // ✅ CHECK EXISTING TASK

        var existingTask = await _context.PutAwayTasks
            .FirstOrDefaultAsync(x =>

                x.ReceiptID == dto.ReceiptID &&
                x.ItemID == dto.ItemID &&
                x.TargetBinID == dto.TargetBinID);


        if (existingTask != null)

        {
            // 🔥 UPDATE TASK
            existingTask.Quantity += dto.Quantity;

        }

        else

        {
            // 🔥 CREATE TASK

            var newTask = new PutAwayTaskModel

            {
                ReceiptID = dto.ReceiptID,
                ItemID = dto.ItemID,
                TargetBinID = dto.TargetBinID,
                Quantity = dto.Quantity,
                //Status = PutAwayStatus.Pending



            };

            _context.PutAwayTasks.Add(newTask);

        }

        // ✅ INVENTORY UPDATE

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

            var newInventory = new InventoryBalanceModel

            {

                ItemID = dto.ItemID,

                BinID = dto.TargetBinID,

                QuantityOnHand = dto.Quantity,

                ReservedQuantity = 0

            };

            _context.InventoryBalances.Add(newInventory);

        }

        await _context.SaveChangesAsync();

        return "PutAway Done ✅";

    }

    // ✅ UPDATE (MANUAL)

    public async Task<PutAwayTaskResponseDTO> Update(int id, UpdatePutAwayTaskDTO dto)

    {

        var existing = await _context.PutAwayTasks.FindAsync(id);

        if (existing == null) return null;

        existing.Quantity = dto.Quantity;

        existing.Status = Enum.Parse<PutAwayStatus>(dto.Status);

        await _context.SaveChangesAsync();

        return new PutAwayTaskResponseDTO

        {

            TaskID = existing.TaskID,
            ReceiptID = existing.ReceiptID,
            ItemID = existing.ItemID,
            TargetBinID = existing.TargetBinID,
            Quantity = existing.Quantity,
            Status = existing.Status.ToString()

        };

    }

    // ✅ DELETE

    public async Task<bool> Delete(int id)

    {

        var existing = await _context.PutAwayTasks.FindAsync(id);

        if (existing == null) return false;

        _context.PutAwayTasks.Remove(existing);

        await _context.SaveChangesAsync();

        return true;

    }

}
