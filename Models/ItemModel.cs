using System.ComponentModel.DataAnnotations;
using WarehousePro.API.Models.Enums;
using WarehouseProject.Models;

public class ItemModel

{

    [Key]

    public int ItemID { get; set; }

    [Required]

    public string Name { get; set; } = string.Empty;

    [Required]

    public string SKU { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string UnitOfMeasure { get; set; } = string.Empty;

    public ItemStatus Status { get; set; }

    // 🔥 RELATIONS

    public ICollection<OrderItemModel>? OrderItems { get; set; }   // ✅ ADD THIS

    public ICollection<InventoryBalanceModel>? InventoryBalances { get; set; }

    public ICollection<PutAwayTaskModel>? PutAwayTasks { get; set; }

    public ICollection<PickTaskModel>? PickTasks { get; set; }

    public ICollection<StockReservationModel>? StockReservations { get; set; }

    public ICollection<ReplenishmentTaskModel>? ReplenishmentTasks { get; set; }

}
