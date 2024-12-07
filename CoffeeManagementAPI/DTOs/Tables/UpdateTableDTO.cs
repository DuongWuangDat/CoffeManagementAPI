using System.ComponentModel.DataAnnotations;

namespace CoffeeManagementAPI.DTOs.Tables
{
    public class UpdateTableDTO : ModifyTable
    {
        [Required]
        public int TableNumber { get; set; }
        [Required]
        public int FloorId { get; set; }

        [Required]
        public int TableTypeID { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
