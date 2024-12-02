using CoffeeManagementAPI.Model;
using System.ComponentModel.DataAnnotations;

namespace CoffeeManagementAPI.DTOs.Tables
{
    public class CreateTableDTO
    {
        [Required]
        public int TableNumber { get; set; }
        [Required]
        public int FloorId { get; set; }
    }
}
