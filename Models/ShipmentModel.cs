using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WarehousePro.API.Models.Enums;

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
        public ShipmentStatus Status { get; set; } = ShipmentStatus.Dispatched;
        
        [ForeignKey("OrderID")]
        public OrderModel? Order { get; set; }
    }
}
