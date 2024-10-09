using CoffeeManagementAPI.DTOs.Customer;
using CoffeeManagementAPI.DTOs.Staff;
using CoffeeManagementAPI.DTOs.Voucher;
using CoffeeManagementAPI.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeManagementAPI.DTOs.Bill
{
    public class BillDTO
    {
        public int BillId { get; set; }

        public string Status { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public CustomerDTO? Customer { get; set; }
        public decimal VoucherValue { get; set; }

        public int VoucherTypeIndex { get; set; }
        public StaffDTO? Staff { get; set; }
        public PayType? PayType { get; set; }

        public List<BillDetailDTO> BillDetailDTOs = new List<BillDetailDTO>();
    }
}
