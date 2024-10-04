using CoffeeManagementAPI.DTOs.Voucher;
using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.Interface
{
    public interface IVoucherRepository
    {

        Task CreateNewVoucher(Voucher newVoucher);
        Task DeleteVoucher(int id);

        Task UpdateVoucher(Voucher voucher, int id);

        Task<List<VoucherDTO>> GetAllVoucher();

        Task<VoucherDTO> GetVoucherById();


    }
}
