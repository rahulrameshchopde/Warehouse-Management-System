using WarehouseProject.DTOs.Outbound;

public interface IShipmentService
{
    Task<ShipmentResponseDto> CreateAsync(ShipmentCreateDto dto);
    Task<List<ShipmentResponseDto>> GetAllAsync();
    Task<ShipmentResponseDto?> UpdateAsync(int id, ShipmentUpdateDto dto);
}