namespace WarehousePro.API.DTOs.Outbound
{
    public class PickTaskResponseDto

    {

        public int PickTaskID { get; set; }

        public int OrderID { get; set; }

        public string OrderNumber { get; set; } = string.Empty;

        public int ItemID { get; set; }

        public string ItemName { get; set; } = string.Empty;

        public int BinID { get; set; }

        public string BinCode { get; set; } = string.Empty;

        public int PickQuantity { get; set; }

        public string Status { get; set; } = string.Empty;

    }
}