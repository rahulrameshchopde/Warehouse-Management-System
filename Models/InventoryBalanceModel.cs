using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseProject.Models
{
    public class InventoryBalanceModel
    {
        [Key]
        public int BalanceID { get; set; }
        public int ItemID { get; set; }
        public int BinID { get; set; }
        public int QuantityOnHand { get; set; }
        public int ReservedQuantity { get; set; }
        [ForeignKey("ItemID")]
        public ItemModel Item { get; set; }
        [ForeignKey("BinID")]
        public BinLocationModel BinLocation { get; set; }
    }
}
