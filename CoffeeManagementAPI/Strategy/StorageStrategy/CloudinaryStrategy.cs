using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using CoffeeManagementAPI.Interface.StrategyInterface;

namespace CoffeeManagementAPI.Strategy.StorageStrategy
{
    public class CloudinaryStrategy : IStorageStrategy
    {
        private readonly Cloudinary _cloudinary;
        IConfiguration _config;
        public CloudinaryStrategy(IConfiguration config)
        {
            _config = config;

            var account = new Account
            {
                Cloud = _config["Cloudinary:CLOUD_NAME"],
                ApiKey = _config["Cloudinary:API_KEY"],
                ApiSecret = _config["Cloudinary:API_SECRET"]
            };

            _cloudinary = new Cloudinary(account);
        }

        public async Task<bool> DeleteImage(string url)
        {
            var id = GetIdFromUri(url);
            if (id == "")
            {
                return false;
            }

            var deleteParams = new DeletionParams(id)
            {
                ResourceType = ResourceType.Image
            };

            await _cloudinary.DestroyAsync(deleteParams);
            return true;
        }

        public async Task<string?> UploadImage(IFormFile file)
        {
            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, stream),
                        Folder = "coffee"
                    };

                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                    return uploadResult.SecureUrl.AbsoluteUri;
                }
            }
            return null;
        }

        string GetIdFromUri(string uri)
        {
            string id = "";
            string startString = "coffee";
            string endString = ".";
            if (uri.Contains(startString) && uri.Contains(endString))
            {
                int start = uri.IndexOf(startString);
                int end = uri.IndexOf(endString, start);
                id = uri.Substring(start, end - start);
            }
            return id;
        }
    }
}
