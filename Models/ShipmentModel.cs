using System.ComponentModel.DataAnnotations;

namespace WarehouseProject.Models
{
    public class ShipmentModel
    {
        [Key]
        public int ShipmentID { get; set; }
        [Required]
        public int OrderID { get; set; }
        [Required]
        public string Carrier { get; set; }
        public DateTime DispatchDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Status { get; set; }
    }
}
