using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeManagementAPI.Model
{
    public class Table
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TableID { get; set; }

        public int TableNumber {  get; set; }

        public int FloorId { get; set; }

        public Floor? Floor { get; set; }

        public string Status { get; set; }
    }
}
