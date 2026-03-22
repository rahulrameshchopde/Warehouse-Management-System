namespace WarehouseProject.DTOs
{
    public class ItemResponseDTO
    {
        public int ItemID { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public string UnitOfMeasure { get; set; }
        public string Status { get; set; }
    }
}