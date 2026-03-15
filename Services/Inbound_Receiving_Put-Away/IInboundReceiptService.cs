using WarehouseProject.DTOs;
using WarehouseProject.Models;
namespace WarehouseProject.Services
{
    public interface IInboundReceiptService
    {
        Task<InboundReceiptModel> Create(InboundReceiptDTO dto);
        Task<List<InboundReceiptModel>> GetAll();
        Task<InboundReceiptModel> GetById(int id);
        Task<bool> Delete(int id);
    }
}