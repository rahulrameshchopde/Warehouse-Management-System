namespace WarehouseProject.DTOs.PutAwayTaskDTOs
{
    public class CreatePutAwayTaskDTO
    {
        public int ReceiptID { get; set; }
        public int ItemID { get; set; }
        public int TargetBinID { get; set; }
        public int Quantity { get; set; }
        
    }
}