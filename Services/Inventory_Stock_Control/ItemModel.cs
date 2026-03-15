using Microsoft.EntityFrameworkCore;

using WarehouseProject.Data;

using WarehouseProject.DTOs;

using WarehouseProject.Models;

using WarehouseProject.Helpers;

namespace WarehouseProject.Services.Inventory_Stock_Control

{

    public class ItemService : IItemService

    {

        private readonly WarehouseDBContext _context;

        private readonly AuditHelper _audit;

        public ItemService(WarehouseDBContext context, AuditHelper audit)

        {

            _context = context;

            _audit = audit;

        }

        public async Task<List<ItemModel>> GetAll()

        {

            return await _context.Items.ToListAsync();

        }

        public async Task<ItemModel> GetById(int id)

        {

            return await _context.Items.FindAsync(id);

        }

        public async Task<ItemModel> Create(ItemDTO dto)

        {

            var item = new ItemModel

            {

                Name = dto.Name,

                SKU = dto.SKU,

                Description = dto.Description,

                UnitOfMeasure = dto.UnitOfMeasure,

                Status = dto.Status

            };

            _context.Items.Add(item);

            await _context.SaveChangesAsync();

            await _audit.LogAction(

                1,

                "CREATE",

                "Item",

                $"Created Item: {item.Name}"

            );

            return item;

        }

        public async Task<ItemModel> Update(int id, ItemDTO dto)

        {

            var item = await _context.Items.FindAsync(id);

            if (item == null)

                return null;

            item.Name = dto.Name;

            item.SKU = dto.SKU;

            item.Description = dto.Description;

            item.UnitOfMeasure = dto.UnitOfMeasure;

            item.Status = dto.Status;

            await _context.SaveChangesAsync();

            await _audit.LogAction(

                1,

                "UPDATE",

                "Item",

                $"Updated Item: {item.Name}"

            );

            return item;

        }

        public async Task<bool> Delete(int id)

        {

            var item = await _context.Items.FindAsync(id);

            if (item == null)

                return false;

            _context.Items.Remove(item);

            await _context.SaveChangesAsync();

            await _audit.LogAction(

                1,

                "DELETE",

                "Item",

                $"Deleted Item: {item.Name}"

            );

            return true;

        }

    }

}
