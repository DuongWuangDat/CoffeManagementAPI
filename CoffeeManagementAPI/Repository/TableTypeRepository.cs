using CoffeeManagementAPI.Data;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace CoffeeManagementAPI.Repository
{
    public class TableTypeRepository : ITableTypeRepository
    {
        private readonly ApplicationDBContext _context;

        public TableTypeRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TableType>> GetAll()
        {
            var tableTypes = await _context.TableTypes.ToListAsync();

            return tableTypes;
        }
    }
}
