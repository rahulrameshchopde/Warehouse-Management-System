using Microsoft.EntityFrameworkCore;
using WarehouseProject.Data;
using WarehouseProject.Models;
namespace WarehouseProject.Services
{
    public class SlottingRuleService : ISlottingRuleService
    {
        private readonly WarehouseDBContext _context;
        public SlottingRuleService(WarehouseDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SlottingRuleModel>> GetAllAsync()
        {
            return await _context.SlottingRules.ToListAsync();
        }
        public async Task<SlottingRuleModel> GetByIdAsync(int id)
        {
            return await _context.SlottingRules.FindAsync(id);
        }
        public async Task<SlottingRuleModel> CreateAsync(SlottingRuleModel rule)
        {
            _context.SlottingRules.Add(rule);
            await _context.SaveChangesAsync();
            return rule;
        }
        public async Task<SlottingRuleModel> UpdateAsync(int id, SlottingRuleModel rule)
        {
            var existing = await _context.SlottingRules.FindAsync(id);
            if (existing == null)
                return null;
            existing.Criterion = rule.Criterion;
            existing.Priority = rule.Priority;
            existing.Status = rule.Status;
            await _context.SaveChangesAsync();
            return existing;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var rule = await _context.SlottingRules.FindAsync(id);
            if (rule == null)
                return false;
            _context.SlottingRules.Remove(rule);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}