using System.ComponentModel.DataAnnotations;

namespace WarehouseProject.Models
{
    public class InboundReceiptModel
    {
        [Key]
        public int ReceiptID { get; set; }
        public string ReferenceNo { get; set; }
        public string Supplier { get; set; }
        public DateTime ReceiptDate { get; set; }
        public string Status { get; set; }
        public ICollection<PutAwayTaskModel> PutAwayTasks { get; set; }
    }
}
