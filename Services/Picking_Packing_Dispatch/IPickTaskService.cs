using WarehousePro.API.DTOs.Outbound;

namespace WarehousePro.API.Services.Interfaces

{

    public interface IPickTaskService

    {

        Task<PickTaskResponseDto> CreatePickAsync(PickTaskCreateDto dto);

        bool DeletePickTask(int id);

        Task <List<PickTaskResponseDto>> GetAllAsync();

        Task<PickTaskResponseDto?> UpdateStatusAsync(int id, PickTaskUpdateDto dto);

        Task AutoCreateFromOrder(int orderId);

    }


}
