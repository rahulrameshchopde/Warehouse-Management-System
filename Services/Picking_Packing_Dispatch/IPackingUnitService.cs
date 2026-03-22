using WarehouseProject.DTOs.Outbound;

public interface IPackingUnitService
{
    Task<PackingResponseDto> CreateAsync(PackingCreateDto dto);
    Task<List<PackingResponseDto>> GetAllAsync();
    Task<PackingResponseDto?> UpdateAsync(int id, PackingStatusUpdateDto dto);



}