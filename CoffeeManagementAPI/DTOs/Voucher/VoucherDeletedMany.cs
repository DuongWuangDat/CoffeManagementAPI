using System.ComponentModel.DataAnnotations;

namespace CoffeeManagementAPI.DTOs.Voucher
{
    public class VoucherDeletedMany
    {
       [Required]
       public IEnumerable<int> setOfVoucherId = [];
    }
}
