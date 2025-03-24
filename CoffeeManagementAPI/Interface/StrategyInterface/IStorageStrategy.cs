namespace CoffeeManagementAPI.Interface.StrategyInterface
{
    public interface IStorageStrategy
    {
        Task<string?> UploadImage(IFormFile file);

        Task<bool> DeleteImage(string url);
    }
}
