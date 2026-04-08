using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WarehousePro.API.Models.Enums;
using WarehouseProject.DTOs.Order;

namespace WarehouseProject.Models
{
    public class OrderModel

    {

        [Key]

        public int OrderID { get; set; }

        [Required, MaxLength(100)]

        public string OrderNumber { get; set; }

        [Required, MaxLength(150)]

        public string CustomerName { get; set; }

        [MaxLength(300)]

        public string? DeliveryAddress { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? RequiredDate { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Created;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        public ICollection<OrderItemModel>? OrderItems { get; set; }
        public ICollection<PickTaskModel>? PickTasks { get; set; }
        public ICollection<PackingUnitModel>? PackingUnits { get; set; }
        public ICollection<ShipmentModel>? Shipments { get; set; }


    }

}
