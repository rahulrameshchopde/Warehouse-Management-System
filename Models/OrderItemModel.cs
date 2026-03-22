using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseProject.Models
{
    public class OrderItemModel

    {

        [Key]

        public int OrderItemID { get; set; }

        public int OrderID { get; set; }

        public int ItemID { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("OrderID")]

        public OrderModel? Order { get; set; }

        [ForeignKey("ItemID")]
        public ItemModel? Item { get; set; }

    }

}
