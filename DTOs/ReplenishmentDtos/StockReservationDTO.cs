namespace WarehouseProject.DTOs.ReplenishmentDtos
{
    public class StockReservationDTO
    {
        public int ItemID { get; set; }
        public string ReferenceType { get; set; }
        public int ReferenceID { get; set; }
        public int Quantity { get; set; }
    }
}