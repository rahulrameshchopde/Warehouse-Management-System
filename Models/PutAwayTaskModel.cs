using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WarehousePro.API.Models.Enums;

namespace WarehouseProject.Models
{
   

public class PutAwayTaskModel

    {

        [Key]

        public int TaskID { get; set; }

        public int ReceiptID { get; set; }

        public int ItemID { get; set; }

        public int TargetBinID { get; set; }

        public int Quantity { get; set; }

        public PutAwayStatus Status { get; set; } = PutAwayStatus.Pending;

        [ForeignKey("ReceiptID")]

        public InboundReceiptModel InboundReceipt { get; set; }

        [ForeignKey("ItemID")]

        public ItemModel Item { get; set; }

        [ForeignKey("TargetBinID")]

        public BinLocationModel BinLocation { get; set; }

    }

}

