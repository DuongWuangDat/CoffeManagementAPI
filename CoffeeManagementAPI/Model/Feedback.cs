using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeManagementAPI.Model
{
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeedbackID { get; set; }

        public int StarNumber { get; set; }

        public string Email { get; set; }

        public string Phonenumber { get; set; }

        public string Fullname { get; set; }

        public string Content { get; set; }


    }
}
