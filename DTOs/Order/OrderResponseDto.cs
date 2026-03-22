namespace WarehouseProject.DTOs.Order
{
    public class OrderResponseDto
    {
        public int OrderID { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string? DeliveryAddress { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public string Status { get; set; } = string.Empty;

    }
}
