using CoffeeManagementAPI.DTOs.Bill;
using CoffeeManagementAPI.Model;
using CoffeeManagementAPI.QueryObject;

namespace CoffeeManagementAPI.Interface
{
    public interface IBillRepository
    {

        Task<bool> CreateNewBill(Bill bill);

        Task DeleteBill(int id);

        Task<BillDTO?> GetBillById(int id);

        Task<List<BillFromGetAllDTO>> GetAllBill();

        Task<List<BillFromGetAllDTO>> GetBillPagination(PaginationObject pagination);

    }
}
