namespace CoffeeManagementAPI.DTOs.Feedback
{
    public class FeedBackDTO
    {
        public int FeedbackId { get; set; } 
        public int StarNumber { get; set; }

        public string Email { get; set; }

        public string Phonenumber { get; set; }

        public string Fullname { get; set; }

        public string Content { get; set; }
    }
}
