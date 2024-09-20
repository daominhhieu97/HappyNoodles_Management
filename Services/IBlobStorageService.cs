using Azure.Storage.Blobs;

namespace HappyNoodles_ManagementApp.Services
{
    public interface IBlobStorageService
    {
        Task<string> UploadFileAsync(byte[] fileBytes, string fileName);
        Task<bool> DeleteBlobAsync(string blobUrl);
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

        public async Task<bool> DeleteBlobAsync(string blobUrl)
        {
            try
            {
                // Extract the container name and blob name from the URL
                Uri uri = new Uri(blobUrl);
                string containerName = GetContainerNameFromUrl(uri);
                string blobName = GetBlobNameFromUrl(uri);

                // Get a reference to the container
                BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

                // Get a reference to the blob
                BlobClient blobClient = containerClient.GetBlobClient(blobName);

                // Delete the blob
                var deleteResponse = await blobClient.DeleteIfExistsAsync();

                // Return true if blob was deleted successfully
                return deleteResponse.Value;
            }
            catch (Exception ex)
            {
                // Handle any errors
                Console.WriteLine($"Error deleting blob: {ex.Message}");
                return false;
            }
        }

        private string GetContainerNameFromUrl(Uri blobUri)
        {
            // Assumes the URL format: https://<accountname>.blob.core.windows.net/<containername>/<blobname>
            // Adjust parsing logic if necessary
            var segments = blobUri.Segments;
            return segments[1].TrimEnd('/'); // Container name
        }

        private string GetBlobNameFromUrl(Uri blobUri)
        {
            // Combine all segments after the container name to get the full blob name
            return string.Join("", blobUri.Segments, 2, blobUri.Segments.Length - 2);
        }
    }
}
