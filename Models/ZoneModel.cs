using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WarehousePro.API.Models.Enums;
namespace WarehouseProject.Models
{
    public class ZoneModel
    {
        [Key]
        public int ZoneID { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int WarehouseID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public ZoneType ZoneType { get; set; }
        [ForeignKey("WarehouseID")]
        public WarehouseModel? Warehouse { get; set; }  // ✅ nullable
        public ICollection<BinLocationModel>? BinLocations { get; set; } // ✅ nullable
    }
}