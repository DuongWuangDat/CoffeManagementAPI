using CoffeeManagementAPI.DTOs.Bill;
using CoffeeManagementAPI.Model;
using System.ComponentModel.DataAnnotations;

namespace CoffeeManagementAPI.DTOs.Tables
{
    public class BookingTableDTO
    {
        [Required]
        public int TableId { get; set; }
        [Required]
        public int BillId { get; set; }

    }
}
