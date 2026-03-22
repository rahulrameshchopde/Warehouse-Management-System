using WarehousePro.API.DTOs.Outbound;

namespace WarehousePro.API.Services.Interfaces

{

    public interface IPickTaskService

    {

        Task<PickTaskResponseDto> CreatePickAsync(PickTaskCreateDto dto);



        Task<List<PickTaskResponseDto>> GetAllAsync();

        Task<PickTaskResponseDto?> UpdateStatusAsync(int id, PickTaskUpdateDto dto);

        Task AutoCreateFromOrder(int orderId);

    }


}
