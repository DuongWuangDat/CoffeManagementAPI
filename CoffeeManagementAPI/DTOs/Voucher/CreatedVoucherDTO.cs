using CoffeeManagementAPI.Model;
using System.ComponentModel.DataAnnotations;

namespace CoffeeManagementAPI.DTOs.Voucher
{
    public class CreatedVoucherDTO
    {
        [Required]
        [MaxLength(20, ErrorMessage ="VocherCode must be less than 20 characters")]
        [MinLength(5, ErrorMessage = "VoucherCode must be 5 characters")]
        public string VoucherCode { get; set; } = string.Empty;
        [Required]
        public decimal VoucherValue { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime ExpiredDate { get; set; }
        [Required]
        public int MaxApply { get; set; }
        [Required]
        public int VoucherTypeId { get; set; }

        public bool IsValidation()
        {
            if(CreatedDate < ExpiredDate)
            {
                return true;
            }
            return false;
        }


    }
}
