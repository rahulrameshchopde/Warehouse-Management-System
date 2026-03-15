using WarehouseProject.Models;
namespace WarehouseProject.Services
{
    public interface ISlottingRuleService
    {
        Task<IEnumerable<SlottingRuleModel>> GetAllAsync();
        Task<SlottingRuleModel> GetByIdAsync(int id);
        Task<SlottingRuleModel> CreateAsync(SlottingRuleModel rule);
        Task<SlottingRuleModel> UpdateAsync(int id, SlottingRuleModel rule);
        Task<bool> DeleteAsync(int id);
    }
}