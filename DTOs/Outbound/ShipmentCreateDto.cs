using WarehousePro.API.Models.Enums;

namespace WarehouseProject.DTOs.Outbound
{
    public class ShipmentCreateDto
    {
        public int OrderID { get; set; }
        public string Carrier { get; set; }
    }
}