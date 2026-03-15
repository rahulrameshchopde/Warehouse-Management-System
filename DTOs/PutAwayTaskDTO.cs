namespace WarehouseProject.DTOs
{
    public class PutAwayTaskDTO
    {
        public int ReceiptID { get; set; }
        public int ItemID { get; set; }
        public int TargetBinID { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
    }
}