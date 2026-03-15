using System.ComponentModel.DataAnnotations;

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
        public string Status { get; set; }

    }
}
