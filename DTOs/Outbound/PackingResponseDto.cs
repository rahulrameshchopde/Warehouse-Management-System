namespace WarehouseProject.DTOs.Outbound
{
    public class PackingResponseDto
    {
        public int PackID { get; set; }
        public int OrderID { get; set; }
        public string PackageType { get; set; }
        public double Weight { get; set; }
        public string Status { get; set; }
    }
}