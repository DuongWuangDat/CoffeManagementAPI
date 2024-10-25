using CoffeeManagementAPI.DTOs.Bill;
using CoffeeManagementAPI.Model;
using CoffeeManagementAPI.QueryObject;

namespace CoffeeManagementAPI.Interface
{
    public interface IBillRepository
    {

        Task<(bool,string)> CreateNewBill(Bill bill);

        Task DeleteBill(int id);

        Task<BillDTO?> GetBillById(int id);

        Task<List<BillDTO>> GetAllBill();

        Task<List<BillDTO>> GetBillPagination(PaginationObject pagination);

        Task<(bool, string)> UpdateStatus(int id, string status);

    }
}
