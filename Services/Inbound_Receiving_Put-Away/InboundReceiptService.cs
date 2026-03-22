using Microsoft.EntityFrameworkCore;
using WarehouseProject.Data;
using WarehouseProject.DTOs.InboundReceipt;
using WarehouseProject.Models;
public class InboundReceiptService : IInboundReceiptService
{
    private readonly WarehouseDBContext _context;
    public InboundReceiptService(WarehouseDBContext context)
    {
        _context = context;
    }
    // ✅ GET ALL
    public async Task<IEnumerable<InboundReceiptResponseDTO>> GetAll()
    {
        return await _context.InboundReceipts
            .Select(x => new InboundReceiptResponseDTO
            {
                ReceiptID = x.ReceiptID,
                ReferenceNo = x.ReferenceNo,
                Supplier = x.Supplier,
                ReceiptDate = x.ReceiptDate,
               
            }).ToListAsync();
    }
    // ✅ GET BY ID
    public async Task<InboundReceiptResponseDTO> GetById(int id)
    {
        return await _context.InboundReceipts
            .Where(x => x.ReceiptID == id)
            .Select(x => new InboundReceiptResponseDTO
            {
                ReceiptID = x.ReceiptID,
                ReferenceNo = x.ReferenceNo,
                Supplier = x.Supplier,
                ReceiptDate = x.ReceiptDate,
                
            }).FirstOrDefaultAsync();
    }
    // ✅ CREATE (NO DUPLICATE ReferenceNo)
    public async Task<InboundReceiptResponseDTO> Create(CreateInboundReceiptDTO dto)
    {
        var exists = await _context.InboundReceipts
            .AnyAsync(x => x.ReferenceNo.ToLower().Trim() == dto.ReferenceNo.ToLower().Trim());
        if (exists) return null;
        var entity = new InboundReceiptModel
        {
            ReferenceNo = dto.ReferenceNo,
            Supplier = dto.Supplier,
            
            ReceiptDate = DateTime.UtcNow
        };
        _context.InboundReceipts.Add(entity);
        await _context.SaveChangesAsync();
        return new InboundReceiptResponseDTO
        {
            ReceiptID = entity.ReceiptID,
            ReferenceNo = entity.ReferenceNo,
            Supplier = entity.Supplier,
            ReceiptDate = entity.ReceiptDate,
            Status = entity.Status.ToString()
            
        };
    }
    // ✅ UPDATE
    public async Task<InboundReceiptResponseDTO> Update(int id, UpdateInboundReceiptDTO dto)
    {
        var existing = await _context.InboundReceipts.FindAsync(id);
        if (existing == null) return null;
        existing.Supplier = dto.Supplier;
        
        await _context.SaveChangesAsync();
        return new InboundReceiptResponseDTO
        {
            ReceiptID = existing.ReceiptID,
            ReferenceNo = existing.ReferenceNo,
            Supplier = existing.Supplier,
            ReceiptDate = existing.ReceiptDate,
           
        };
    }
    // ✅ DELETE
    public async Task<bool> Delete(int id)
    {
        var existing = await _context.InboundReceipts.FindAsync(id);
        if (existing == null) return false;
        _context.InboundReceipts.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}