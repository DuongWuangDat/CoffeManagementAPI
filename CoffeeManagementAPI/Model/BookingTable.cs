using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeManagementAPI.Model
{
    public class BookingTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingTableID { get; set; }

        public int TableId { get; set; }

        public Table? Table { get; set; }

        public int BillId { get; set; }

        public Bill? Bill { get; set; }

    }
}
