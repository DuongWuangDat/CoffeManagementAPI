using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeManagementAPI.Model
{
    public class Token
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TokenId { get; set; }
        public string TokenName { get; set; } = string.Empty;
        public bool IsRevoked { get; set; } = false;
    }
}
