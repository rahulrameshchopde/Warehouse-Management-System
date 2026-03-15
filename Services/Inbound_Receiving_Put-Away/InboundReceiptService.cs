using Microsoft.EntityFrameworkCore;
using WarehouseProject.Data;
using WarehouseProject.DTOs;
using WarehouseProject.Models;
namespace WarehouseProject.Services
{
    public class InboundReceiptService : IInboundReceiptService
    {
        private readonly WarehouseDBContext _context;
        public InboundReceiptService(WarehouseDBContext context)
        {
            _context = context;
        }
        public async Task<InboundReceiptModel> Create(InboundReceiptDTO dto)
        {
            var receipt = new InboundReceiptModel
            {
                ReferenceNo = dto.ReferenceNo,
                Supplier = dto.Supplier,
                Status = dto.Status,
                ReceiptDate = DateTime.UtcNow
            };
            _context.InboundReceipts.Add(receipt);
            await _context.SaveChangesAsync();
            return receipt;
        }
        public async Task<List<InboundReceiptModel>> GetAll()
        {
            return await _context.InboundReceipts
                .Include(x => x.PutAwayTasks)
                .ToListAsync();
        }
        public async Task<InboundReceiptModel> GetById(int id)
        {
            return await _context.InboundReceipts
                .Include(x => x.PutAwayTasks)
                .FirstOrDefaultAsync(x => x.ReceiptID == id);
        }
        public async Task<bool> Delete(int id)
        {
            var receipt = await _context.InboundReceipts.FindAsync(id);
            if (receipt == null)
                return false;
            _context.InboundReceipts.Remove(receipt);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}