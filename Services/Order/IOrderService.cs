using WarehouseProject.DTOs.Order;

namespace WarehouseProject.Services.Order
{
    public interface IOrderService
    {
        Task<OrderResponseDto> CreateAsync(OrderCreateDto dto);
        Task<List<OrderResponseDto>> GetAllAsync();
        Task<OrderResponseDto?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, OrderUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        // FLOW
        Task<bool> StartPicking(int id);
        Task<bool> CompletePicking(int id);
        Task<bool> Ship(int id);
        Task<bool> Deliver(int id);
    }
}
