using System.ComponentModel.DataAnnotations;

namespace CoffeeManagementAPI.DTOs.Tables
{
    public class ModifyTable
    {
        private readonly IEnumerable<string> statusList = new List<string> { 
            "Booked", "Not booked", "Under repair"
        };
        [Required]
        public string Status { get; set; }

        public bool isStatucCorrect()
        {
            return statusList.Contains(Status);
        }
    }
}
