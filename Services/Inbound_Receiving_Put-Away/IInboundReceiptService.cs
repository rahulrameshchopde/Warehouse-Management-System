using WarehouseProject.DTOs.InboundReceipt;

public interface IInboundReceiptService
{
    Task<IEnumerable<InboundReceiptResponseDTO>> GetAll();
    Task<InboundReceiptResponseDTO> GetById(int id);
    Task<InboundReceiptResponseDTO> Create(CreateInboundReceiptDTO dto);
    Task<InboundReceiptResponseDTO> Update(int id, UpdateInboundReceiptDTO dto);
    Task<bool> Delete(int id);
}