using WarehousePro.API.Models.Enums;

namespace WarehouseProject.DTOs.Outbound
{
    public class ShipmentUpdateDto
    {
        public ShipmentStatus Status { get; set; }
        public DateTime? DeliveryDate { get; set; }
    }
}
