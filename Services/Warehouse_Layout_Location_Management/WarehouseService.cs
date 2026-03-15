using WarehouseProject.Data;
using WarehouseProject.DTOs;
using WarehouseProject.Models;
using Microsoft.EntityFrameworkCore;

namespace WarehouseProject.Services.Warehouse_Layout_Location_Management
{

        public class WarehouseService : IWarehouseService

        {

            private readonly WarehouseDBContext _context;

            public WarehouseService(WarehouseDBContext context)

            {

                _context = context;

            }

            public async Task<IEnumerable<WarehouseModel>> GetAll()

            {

                return await _context.Warehouses.ToListAsync();

            }

            public async Task<WarehouseModel> GetById(int id)

            {

                return await _context.Warehouses.FindAsync(id);

            }

            public async Task<WarehouseModel> Create(WarehouseDTO dto)

            {

                var warehouse = new WarehouseModel

                {

                    Name = dto.Name,

                    Location = dto.Location,

                    Status = dto.Status

                };

                _context.Warehouses.Add(warehouse);

                await _context.SaveChangesAsync();

                return warehouse;

            }

            public async Task<bool> Delete(int id)

            {

                var warehouse = await _context.Warehouses.FindAsync(id);

                if (warehouse == null)

                    return false;

                _context.Warehouses.Remove(warehouse);

                await _context.SaveChangesAsync();

                return true;

            }

        }

    }
 