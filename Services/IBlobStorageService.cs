using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace HappyNoodles_ManagementApp.Services
{
    public interface IBlobStorageService
    {
        Task<string> UploadFileAsync(byte[] fileBytes, string fileName);
    }

    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public BlobStorageService(IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("AzureStorage")["ConnectionString"];
            _containerName = configuration.GetSection("AzureStorage")["ContainerName"];
            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task<string> UploadFileAsync(byte[] fileBytes, string fileName)
        {
            // Get a reference to the container
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);

            // Get a reference to the blob (file)
            var blobClient = containerClient.GetBlobClient(fileName);

            // Upload the byte array as a stream
            using (var memoryStream = new MemoryStream(fileBytes))
            {
                await blobClient.UploadAsync(memoryStream, overwrite: true);
            }

            // Return the URL of the uploaded blob
            return blobClient.Uri.ToString();
        }
    }
}
