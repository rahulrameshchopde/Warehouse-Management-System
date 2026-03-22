using System.ComponentModel.DataAnnotations;
using WarehousePro.API.Models.Enums;

namespace WarehouseProject.Models
{
    public class WarehouseModel
    {
        [Key]
        public int WarehouseID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        public WarehouseStatus Status { get; set; }
        public ICollection<ZoneModel> Zones { get; set; }
    }
}
