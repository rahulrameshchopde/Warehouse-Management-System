namespace WarehouseProject.DTOs.Outbound
{
    public class ShipmentResponseDto
    {
        public int ShipmentID { get; set; }
        public int OrderID { get; set; }
        public string Carrier { get; set; }
        public DateTime DispatchDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string Status { get; set; }
    }
}