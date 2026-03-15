namespace WarehouseProject.DTOs
{
    public class InventoryBalanceDTO
    {
        public int ItemID { get; set; }
        public int BinID { get; set; }
        public int QuantityOnHand { get; set; }
        public int ReservedQuantity { get; set; }
    }
}