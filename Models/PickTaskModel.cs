using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseProject.Models
{
    public class PickTaskModel
    {
        [Key]
        public int PickTaskID { get; set; }
        public int OrderID { get; set; }
        public int ItemID { get; set; }
        public int BinID { get; set; }
        public int PickQuantity { get; set; }
       
        public string Status { get; set; }

        [ForeignKey("ItemID")]
       public ItemModel Item { get; set; }

        [ForeignKey("BinID")]
        public BinLocationModel BinLocation { get; set; }

    }
}
