namespace WarehouseProject.DTOs
{
    public class ShipmentDTO
    {
        public int OrderID { get; set; }
        public string Carrier { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}