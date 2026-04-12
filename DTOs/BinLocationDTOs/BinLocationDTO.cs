using System.ComponentModel.DataAnnotations;

namespace WarehouseProject.DTOs.BinLocationDTOs
{
    public class BinLocationDTO
    {
        [Required]
        public int ZoneID { get; set; }
        public string Code { get; set; }
        public int Capacity { get; set; }
        public string Status { get; set; }
    }
}