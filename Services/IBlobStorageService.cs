using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace HappyNoodles_ManagementApp.Services
{
    public interface IBlobStorageService
    {
        Task<string> UploadFileAsync(Stream fileStream, string fileName);
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

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName)
        {
            // Get the blob container client
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);

            // Create the container if it doesn't exist
            await blobContainerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

            // Get the blob client for the uploaded file
            var blobClient = blobContainerClient.GetBlobClient(fileName);

            // Upload the file
            await blobClient.UploadAsync(fileStream, true);

            // Return the URI to access the file
            return blobClient.Uri.ToString();
        }
    }
}
