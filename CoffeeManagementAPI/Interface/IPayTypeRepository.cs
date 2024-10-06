using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.Interface
{
    public interface IPayTypeRepository
    {
        Task<List<PayType>> GetAll();
    }
}
