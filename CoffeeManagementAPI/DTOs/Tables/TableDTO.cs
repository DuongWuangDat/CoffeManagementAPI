using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.DTOs.Tables
{
    public class TableDTO
    {

        public int TableID { get; set; }

        public int TableNumber { get; set; }

        public int FloorId { get; set; }

        public Floor? Floor { get; set; }

        public string Status { get; set; }

    }
}
