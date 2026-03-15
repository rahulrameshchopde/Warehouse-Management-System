using System.ComponentModel.DataAnnotations;

namespace WarehouseProject.Models
{
    public class WarehouseModel
    {
        [Key]
        public int WarehouseID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public ICollection<ZoneModel> Zones { get; set; }
    }
}
