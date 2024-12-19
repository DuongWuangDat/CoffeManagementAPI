namespace CoffeeManagementAPI.DTOs.SendEmail
{
    public class EmailResult
    {
        public string Email { get; set; }
        public bool Success { get; set; }
        public string ErrorMsg { get; set; }

        public string Voucher {  get; set; }
    }
}
