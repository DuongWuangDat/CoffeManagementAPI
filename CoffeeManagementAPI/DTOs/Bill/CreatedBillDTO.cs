using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CoffeeManagementAPI.DTOs.Bill
{
    public class CreatedBillDTO
    {


        [Required]
        public string Status { get; set; } = string.Empty;
        [Required]
        public decimal TotalPrice { get; set; }

        public int? CustomerId { get; set; }

        public int? VoucherId { get; set; }

        [Required]
        public int StaffId { get; set; }
        [Required]
        public int PayTypeId { get; set; }
        [Required]
        public List<CreateBillDetailDTO> BillDetails { get; set; } = new List<CreateBillDetailDTO>();

    }
}
