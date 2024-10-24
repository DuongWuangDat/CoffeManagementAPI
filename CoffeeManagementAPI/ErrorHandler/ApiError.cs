namespace CoffeeManagementAPI.ErrorHandler
{
    public class ApiError
    {
        public ApiError(string msg) { 
            message = msg;
        }
        public string message { get; set; }
    }
}
