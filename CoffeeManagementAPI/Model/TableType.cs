using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeManagementAPI.Model
{
    public class TableType
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TableTypeID { get; set; }

        public string TableNameType { get; set; }

        public List<Table> Tables { get; set; } = new List<Table>();

    }
}
