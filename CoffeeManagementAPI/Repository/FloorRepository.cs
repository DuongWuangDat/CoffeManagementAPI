
using CoffeeManagementAPI.Data;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace CoffeeManagementAPI.Repository
{
    public class FloorRepository : IFloorRepository
    {
        private readonly ApplicationDBContext _context;
        public FloorRepository(ApplicationDBContext context)
        {

            _context = context;

        }
        public async Task<(bool, string)> CreateNewFloor(Floor newFloor)
        {
            await _context.Floors.AddAsync(newFloor);
            await _context.SaveChangesAsync();
            return (true, "");
        }

        public async Task<(bool, string)> DeleteFloor(int id)
        {
           var floor = await _context.Floors.FindAsync(id);
            if (floor == null)
            {
                return (false, "Floor is not found");
            }
            _context.Floors.Remove(floor);
            await _context.SaveChangesAsync();
            return (true, "");
        }

        public async Task<IEnumerable<Floor>> GetAllFloor()
        {
            var floorList = await _context.Floors.Include(f=> f.Tables).ThenInclude(t=> t.TableType).ToListAsync();
            return floorList;
        }
    }
}
