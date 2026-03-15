namespace WarehouseProject.DTOs
{
    public class DashboardDTO
    {
        public int TotalItems { get; set; }
        public int TotalWarehouses { get; set; }
        public int TotalZones { get; set; }
        public int TotalBins { get; set; }
        public int TotalPickTasks { get; set; }
        public int TotalShipments { get; set; }
        public int TotalReplenishments { get; set; }
    }
}