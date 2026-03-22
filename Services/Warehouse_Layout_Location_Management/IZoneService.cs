using WarehousePro.API.DTOs.Zone;

public interface IZoneService
{
    Task<List<ZoneResponseDto>> GetAllAsync();
    Task<ZoneResponseDto?> GetByIdAsync(int id);
    Task<ZoneResponseDto> CreateAsync(ZoneCreateDto dto);
    Task<bool> UpdateAsync(int id, ZoneUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}