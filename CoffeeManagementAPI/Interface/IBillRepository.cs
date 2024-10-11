using CoffeeManagementAPI.DTOs.Bill;
using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.Interface
{
    public interface IBillRepository
    {

        Task<bool> CreateNewBill(Bill bill);

        Task DeleteBill(int id);

        Task<BillDTO?> GetBillById(int id);

        Task<List<BillFromGetAllDTO>> GetAllBill();

    }
}
