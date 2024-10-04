using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.Interface
{
    public interface IBillRepository
    {

        Task CreateNewBill(Bill bill);

        Task DeleteBill(int id);

        Task<Bill> GetBillById(int id);

        Task<Bill> GetAllBill();

    }
}
