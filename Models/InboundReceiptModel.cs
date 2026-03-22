using System.ComponentModel.DataAnnotations;
using WarehousePro.API.Models.Enums;

namespace WarehouseProject.Models
{
    public class InboundReceiptModel
    {
        [Key]
        public int ReceiptID { get; set; }
        [Required]
        public string ReferenceNo { get; set; }
        [Required]
        public string Supplier { get; set; }
        [Required]
        public DateTime ReceiptDate { get; set; }

        public ReceiptStatus Status { get; set; } = ReceiptStatus.Received;
        public ICollection<PutAwayTaskModel> PutAwayTasks { get; set; }
    }
}
