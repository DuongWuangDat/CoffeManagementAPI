using CoffeeManagementAPI.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeManagementAPI.DTOs.Voucher
{
    public class VoucherDTO
    {

        public int VoucherID { get; set; }

        public string VoucherCode { get; set; } = string.Empty;
        public decimal VoucherValue { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ExpiredDate { get; set; }
        public int MaxApply { get; set; }
        public VoucherType? VoucherType { get; set; }
    }
}
