namespace WarehouseProject.DTOs.BinLocationDTOs
{
    public class BinLocationResponseDTO
    {
        public int BinID { get; set; }
        public int ZoneID { get; set; }
        public string Code { get; set; }
        public int Capacity { get; set; }
        public string Status { get; set; }
    }
}