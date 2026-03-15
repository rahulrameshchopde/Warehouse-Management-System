using System.ComponentModel.DataAnnotations;

namespace WarehouseProject.Models
{
    public class SlottingRuleModel
    {
        [Key]
        public int RuleID { get; set; }
        public string Criterion { get; set; }
        public int Priority { get; set; }
        public string Status { get; set; }
    }
}
