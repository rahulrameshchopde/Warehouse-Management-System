using System.ComponentModel.DataAnnotations.Schema;
using WarehouseProject.Models;

namespace WarehouseProject.DTOs
{
    public class PickTaskDTO
    {
        public int OrderID { get; set; }
        public int ItemID { get; set; }
        public int BinID { get; set; }
        public int PickQuantity { get; set; }
        public string Status { get; set; }

        

    }
}