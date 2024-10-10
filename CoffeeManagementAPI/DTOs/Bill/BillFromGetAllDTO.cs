using CoffeeManagementAPI.DTOs.Customer;

namespace CoffeeManagementAPI.DTOs.Bill
{
    public class BillFromGetAllDTO
    {
        public int BillId { get; set; }

        public string Status { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public DateTime? CreatedAt { get; set; }

    }
}
