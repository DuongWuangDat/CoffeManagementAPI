using CoffeeManagementAPI.DTOs.Voucher;
using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.Interface
{
    public interface IVoucherRepository
    {

        Task<bool> CreateNewVoucher(Voucher newVoucher);
        Task<bool> DeleteVoucher(int id);

        Task<bool> UpdateVoucher(Voucher voucher, int id);

        Task<List<VoucherDTO>> GetAllVoucher();

        Task<VoucherDTO?> GetVoucherById(int id);


    }
}
