using WarehouseProject.DTOs;
using WarehouseProject.Models;
namespace WarehouseProject.Services
{
    public interface IPutAwayService
    {
        Task<PutAwayTaskModel> Create(PutAwayTaskDTO dto);
        Task<List<PutAwayTaskModel>> GetAll();
        Task<PutAwayTaskModel> CompleteTask(int taskId);
    }
}