using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseProject.Models
{
    public class BinLocationModel
    {
        [Key]
        public int BinID { get; set; }
        public int ZoneID { get; set; }
        public string Code { get; set; }
        public int Capacity { get; set; }
        public string Status { get; set; }
       
        [ForeignKey("ZoneID")]
        public ZoneModel Zone { get; set; }
        public ICollection<InventoryBalanceModel> InventoryBalances { get; set; }
        public ICollection<PutAwayTaskModel> PutAwayTasks { get; set; }
        public ICollection<PickTaskModel> PickTasks { get; set; }
    }
}
