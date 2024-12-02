using CoffeeManagementAPI.DTOs.Tables;
using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.Interface
{
    public interface IFloorRepository
    {

        Task<IEnumerable<Floor>> GetAllFloor();

        Task<(bool, string)> CreateNewFloor(Floor newFloor);

        Task<(bool, string)> DeleteFloor(int id);
    }
}
