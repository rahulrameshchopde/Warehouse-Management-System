using System.ComponentModel.DataAnnotations;
using WarehousePro.API.Models.Enums;

namespace WarehouseProject.Models
{
    public class SlottingRuleModel
    {
        [Key]
        public int RuleID { get; set; }
        public SlottingCriterion Criterion { get; set; }
        public int Priority { get; set; }
        public SlottingRuleStatus Status { get; set; } = SlottingRuleStatus.Active;
    }
}
