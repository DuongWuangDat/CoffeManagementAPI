namespace CoffeeManagementAPI.Interface
{
    public interface ICloudinaryService
    {

        Task<string?> UploadImage(IFormFile file);

        Task<bool> DeleteImage(string url);

    }
}
