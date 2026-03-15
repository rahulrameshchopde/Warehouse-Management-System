using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseProject.Models
{
    public class ZoneModel
    {
        [Key]
        public int ZoneID { get; set; }
        public int WarehouseID { get; set; }
        public string Name { get; set; }
        public string ZoneType { get; set; }
        
        [ForeignKey("WarehouseID")]
        public WarehouseModel Warehouse { get; set; }
        public ICollection<BinLocationModel> BinLocations { get; set; }
    }
}
