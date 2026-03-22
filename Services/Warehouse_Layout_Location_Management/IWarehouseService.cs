using WarehousePro.API.DTOs.Warehouse;

public interface IWarehouseService

{

    Task<List<WarehouseResponseDto>> GetAllAsync();

    Task<WarehouseResponseDto?> GetByIdAsync(int id);

    Task<WarehouseResponseDto> CreateAsync(WarehouseCreateDto dto);

    Task<WarehouseResponseDto> UpdateAsync(int id, WarehouseUpdateDto dto);

    Task<bool> DeleteAsync(int id);

}
