using Microsoft.EntityFrameworkCore;

using WarehouseProject.Data;

using WarehouseProject.DTOs;

using WarehouseProject.Models;

namespace WarehouseProject.Services

{

    public class StockReservationService : IStockReservationService

    {

        private readonly WarehouseDBContext _context;

        public StockReservationService(WarehouseDBContext context)

        {

            _context = context;

        }

        public async Task<StockReservationModel> Create(StockReservationDTO dto)

        {

            var reservation = new StockReservationModel

            {

                ItemID = dto.ItemID,

                ReferenceType = dto.ReferenceType,

                ReferenceID = dto.ReferenceID,

                Quantity = dto.Quantity

            };

            _context.StockReservations.Add(reservation);

            var inventory = await _context.InventoryBalances

                .FirstOrDefaultAsync(x => x.ItemID == dto.ItemID);

            if (inventory != null)

            {

                inventory.ReservedQuantity += dto.Quantity;

            }

            await _context.SaveChangesAsync();

            return await _context.StockReservations

                .Include(x => x.item)

                .FirstOrDefaultAsync(x => x.ReservationID == reservation.ReservationID);

        }

        public async Task<List<StockReservationModel>> GetAll()

        {

            return await _context.StockReservations

                .Include(x => x.item)

                .ToListAsync();

        }

        public async Task<bool> Delete(int id)

        {

            var reservation = await _context.StockReservations

                .FirstOrDefaultAsync(x => x.ReservationID == id);

            if (reservation == null)

                return false;

            var inventory = await _context.InventoryBalances

                .FirstOrDefaultAsync(x => x.ItemID == reservation.ItemID);

            if (inventory != null)

            {

                inventory.ReservedQuantity -= reservation.Quantity;

            }

            _context.StockReservations.Remove(reservation);

            await _context.SaveChangesAsync();

            return true;

        }

    }

}
