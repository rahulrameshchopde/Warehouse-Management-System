using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WarehousePro.API.Models.Enums;
namespace WarehouseProject.Models
{
    public class BinLocationModel
    {
        [Key]
        public int BinID { get; set; }
        [Required]
        public int ZoneID { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public int Capacity { get; set; }
        [Required]
        public BinStatus Status { get; set; } = BinStatus.Available;
        [ForeignKey("ZoneID")]
        public ZoneModel? Zone { get; set; }
        public ICollection<InventoryBalanceModel>? InventoryBalances { get; set; }
        public ICollection<PutAwayTaskModel>? PutAwayTasks { get; set; }
        public ICollection<PickTaskModel>? PickTasks { get; set; }
    }
}