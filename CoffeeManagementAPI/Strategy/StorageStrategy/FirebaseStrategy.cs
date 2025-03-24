using CoffeeManagementAPI.Interface.StrategyInterface;

namespace CoffeeManagementAPI.Strategy.StorageStrategy
{
    public class FirebaseStrategy : IStorageStrategy
    {
        public Task<bool> DeleteImage(string url)
        {
            throw new NotImplementedException();
        }

        public Task<string?> UploadImage(IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
