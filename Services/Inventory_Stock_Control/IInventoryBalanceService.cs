using WarehousePro.API.DTOs.Outbound;
using WarehouseProject.DTOs;
using WarehouseProject.DTOs.InventoryBalanceDTOs;
using WarehouseProject.DTOs.Outbound;
using WarehouseProject.DTOs.PutAwayTaskDTOs;
public interface IInventoryBalanceService
{
    Task<IEnumerable<InventoryBalanceResponseDTO>> GetAll();
    Task<InventoryBalanceResponseDTO> GetById(int id);
    Task<InventoryBalanceResponseDTO> Create(CreateInventoryBalanceDTO dto);
    Task<InventoryBalanceResponseDTO> Update(int id, UpdateInventoryBalanceDTO dto);
    Task<bool> Delete(int id);

    Task<string> PutAwayAsync(CreatePutAwayTaskDTO dto);
    Task<string> CreatePickAsync (PickTaskCreateDto dto);

    
}