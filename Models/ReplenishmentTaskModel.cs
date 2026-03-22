using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WarehousePro.API.Models.Enums;

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
        public ReplenishmentStatus Status { get; set; } = ReplenishmentStatus.Planned;

        [ForeignKey("ItemID")]
        public ItemModel Item { get; set; }

        [ForeignKey("FromBinID")]
        public BinLocationModel FromBin { get; set; }

        [ForeignKey("ToBinID")]
        public BinLocationModel ToBin{ get; set; }
    }
}
