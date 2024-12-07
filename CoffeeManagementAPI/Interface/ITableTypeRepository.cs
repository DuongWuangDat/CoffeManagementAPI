using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.Interface
{
    public interface ITableTypeRepository
    {

        Task<IEnumerable<TableType>> GetAll();
    }
}
