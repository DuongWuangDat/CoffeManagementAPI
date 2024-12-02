using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeManagementAPI.Model
{
    public class Floor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FloorID { get; set; }

        public int FloorNumber { get; set; }

        public List<Table> Tables {  get; set; } = new List<Table>();

    }
}
