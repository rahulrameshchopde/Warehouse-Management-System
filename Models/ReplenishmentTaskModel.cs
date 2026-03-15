using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseProject.Models
{
    public class ReplenishmentTaskModel
    {
        [Key]
        public int ReplenishID { get; set; }
        [Required]
        public int ItemID { get; set; }
        [Required]
        public int FromBinID { get; set; }
        [Required]
        public int ToBinID { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string Status { get; set; }

        [ForeignKey("ItemID")]
        public ItemModel Item { get; set; }

        [ForeignKey("FromBinID")]
        public BinLocationModel FromBin { get; set; }

        [ForeignKey("ToBinID")]
        public BinLocationModel ToBin{ get; set; }
    }
}
