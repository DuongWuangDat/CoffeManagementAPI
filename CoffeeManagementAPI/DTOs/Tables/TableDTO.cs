using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.DTOs.Tables
{
    public class TableDTO
    {

        public int TableID { get; set; }

        public int TableNumber { get; set; }

        public int? TableTypeID { get; set; }

        public TableType? TableType { get; set; }

        public int FloorId { get; set; }

        public FloorDTO? Floor { get; set; }

        public string Status { get; set; }

    }
}
