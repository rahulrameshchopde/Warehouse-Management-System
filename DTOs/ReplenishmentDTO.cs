namespace WarehouseProject.DTOs
{
    public class ReplenishmentDTO
    {
        public int ItemID { get; set; }
        public int FromBinID { get; set; }
        public int ToBinID { get; set; }
        public int Quantity { get; set; }
    }
}