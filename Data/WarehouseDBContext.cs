using Microsoft.EntityFrameworkCore;
using WarehouseProject.Models;

namespace WarehouseProject.Data
{
    public class WarehouseDBContext : DbContext
    {
           
        public WarehouseDBContext(DbContextOptions<WarehouseDBContext> options) : base(options)

        {

        }


        // User Module

        public DbSet<UserModel> Users { get; set; }

        public DbSet<AuditLogModel> AuditLogs { get; set; }

        public DbSet<NotificationModel> Notifications { get; set; }

        // Warehouse Layout

        public DbSet<WarehouseModel> Warehouses { get; set; }

        public DbSet<ZoneModel> Zones { get; set; }

        public DbSet<BinLocationModel> BinLocations { get; set; }

        // Inventory

        public DbSet<ItemModel> Items { get; set; }

        public DbSet<InventoryBalanceModel> InventoryBalances { get; set; }

        public DbSet<StockReservationModel> StockReservations { get; set; }

        // Inbound

        public DbSet<InboundReceiptModel> InboundReceipts { get; set; }

        public DbSet<PutAwayTaskModel> PutAwayTasks { get; set; }
        //Order


        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<OrderItemModel> OrderItems { get; set; }

        // Picking / Packing



        public DbSet<PickTaskModel> PickTasks { get; set; }

        public DbSet<PackingUnitModel> PackingUnits { get; set; }

        // Shipping

        public DbSet<ShipmentModel> Shipments { get; set; }

        // Replenishment

        public DbSet<ReplenishmentTaskModel> ReplenishmentTasks { get; set; }

        // Rules / Reports

        public DbSet<SlottingRuleModel> SlottingRules { get; set; }

        public DbSet<WarehouseReportModel> WarehouseReports { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReplenishmentTaskModel>()
                .HasOne(r => r.FromBin)
                .WithMany()
                .HasForeignKey(r => r.FromBinID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ReplenishmentTaskModel>()
                .HasOne(r => r.ToBin)
                .WithMany()
                .HasForeignKey(r => r.ToBinID)
                .OnDelete(DeleteBehavior.Restrict);
        }
        


    }

}

   
