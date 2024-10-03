namespace CoffeeManagementAPI.DTOs.Staff
{
    public class StaffDTO
    {
        public int StaffId { get; set; }

        public string StaffName { get; set; } = string.Empty;

        public bool IsAdmin { get; set; } = false;
        public string Username { get; set; } = string.Empty;

    }
}
