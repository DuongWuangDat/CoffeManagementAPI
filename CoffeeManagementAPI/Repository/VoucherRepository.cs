using CoffeeManagementAPI.DTOs.Voucher;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.Repository
{
    public class VoucherRepository : IVoucherRepository
    {
        public Task CreateNewVoucher(Voucher newVoucher)
        {
            throw new NotImplementedException();
        }

        public Task DeleteVoucher(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<VoucherDTO>> GetAllVoucher()
        {
            throw new NotImplementedException();
        }

        public Task<VoucherDTO> GetVoucherById()
        {
            throw new NotImplementedException();
        }

        public Task UpdateVoucher(Voucher voucher, int id)
        {
            throw new NotImplementedException();
        }
    }
}
