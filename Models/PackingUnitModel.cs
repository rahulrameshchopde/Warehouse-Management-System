using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WarehousePro.API.Models.Enums;

namespace WarehouseProject.Models
{
    public class PackingUnitModel
    {
        [Key]
        public int PackID { get; set; }
        [Required]
        public int OrderID { get; set; }
        [Required]
        public string PackageType { get; set; }

        [Required]
        public double Weight { get; set; }
        public PackingStatus Status { get; set; } = PackingStatus.Packed;
       
        [ForeignKey("OrderID")]
        public OrderModel? Order { get; set; }

    }
}
