using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.Repository
{
    public class BillRepository : IBillRepository
    {
        public Task CreateNewBill(Bill bill)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBill(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Bill> GetAllBill()
        {
            throw new NotImplementedException();
        }

        public Task<Bill> GetBillById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
