using WarehouseProject.Models;
using WarehouseProject.DTOs.ReplenishmentDtos;
public interface IStockReservationService
{
    Task<StockReservationModel> Create(StockReservationDTO dto);
    Task<List<StockReservationModel>> GetAll();
    Task<bool> Delete(int id);
}