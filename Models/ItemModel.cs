using System.ComponentModel.DataAnnotations;

namespace WarehouseProject.Models
{
    public class ItemModel
    {
        [Key]
        public int ItemID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string SKU { get; set; }
        public string Description { get; set; }
        public string UnitOfMeasure { get; set; }
        public string Status { get; set; }
        public ICollection<InventoryBalanceModel> InventoryBalances { get; set; }
        public ICollection<PutAwayTaskModel> PutAwayTasks { get; set; }
        public ICollection<PickTaskModel> PickTasks { get; set; }
        public ICollection<StockReservationModel> StockReservations { get; set; }
        public ICollection<ReplenishmentTaskModel> ReplenishmentTasks { get; set; }

    }
}
