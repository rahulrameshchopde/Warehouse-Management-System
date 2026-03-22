using WarehouseProject.DTOs;
using WarehouseProject.DTOs.itemDtos;
public interface IItemService
{
    Task<IEnumerable<ItemResponseDTO>> GetAll();
    Task<ItemResponseDTO> GetById(int id);
    Task<ItemResponseDTO> Create(CreateItemDTO dto);
    Task<ItemResponseDTO> Update(int id, UpdateItemDTO dto);
    Task<bool> Delete(int id);
}