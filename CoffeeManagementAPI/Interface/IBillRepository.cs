using CoffeeManagementAPI.DTOs.Bill;
using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.Interface
{
    public interface IBillRepository
    {

        Task CreateNewBill(Bill bill);

        Task DeleteBill(int id);

        Task<List<BillFromGetAllDTO>> GetBillById(int id);

        Task<BillDTO?> GetAllBill();

    }
}
