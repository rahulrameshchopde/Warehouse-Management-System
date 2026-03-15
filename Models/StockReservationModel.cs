using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseProject.Models
{
    public class StockReservationModel
    {
        [Key]
        public int ReservationID { get; set; }
        public int ItemID { get; set; }
        public string ReferenceType { get; set; }
        public int ReferenceID { get; set; }
        public int Quantity { get; set; }
        

        [ForeignKey("ItemID")]
        public ItemModel item { get; set; }

        }
    }
