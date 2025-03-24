using CoffeeManagementAPI.Interface.StrategyInterface;
using CoffeeManagementAPI.Strategy.StorageStrategy;

namespace CoffeeManagementAPI.Factory
{
    public class StorageFactory
    {

        private readonly IServiceProvider _serviceProvider;

        public StorageFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IStorageStrategy GetStorageStragery(string storageType)
        {
            return storageType switch
            {
                "CLOUDINARY" => _serviceProvider.GetRequiredService<CloudinaryStrategy>(),
                "FIREBASE" => _serviceProvider.GetRequiredService<FirebaseStrategy>(),
                _ => throw new NotSupportedException()
            };
        }


    }
}
