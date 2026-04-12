namespace WarehouseProject.DTOs
{
    public class InventoryBalanceResponseDTO
    {
        public int BalanceID { get; set; }
        public int ItemID { get; set; }
        public int BinID { get; set; }

        public string ItemName { get; set; }

        public string BinCode { get; set; }
        public int QuantityOnHand { get; set; }
        public int ReservedQuantity { get; set; }
    }
}