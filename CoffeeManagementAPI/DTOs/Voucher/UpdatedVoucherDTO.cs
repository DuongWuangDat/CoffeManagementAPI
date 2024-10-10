using System.ComponentModel.DataAnnotations;

namespace CoffeeManagementAPI.DTOs.Voucher
{
    public class UpdatedVoucherDTO
    {
        [Required]
        [MaxLength(20, ErrorMessage = "VocherCode must be less than 20 characters")]
        [MinLength(5, ErrorMessage = "VoucherCode must be 5 characters")]
        public string VoucherCode { get; set; } = string.Empty;
        [Required]
        public decimal VoucherValue { get; set; }
        [Required]
        public DateTime ExpiredDate { get; set; }
        [Required]
        public int MaxApply { get; set; }
        [Required]
        public int VoucherTypeId { get; set; }
    }
}
