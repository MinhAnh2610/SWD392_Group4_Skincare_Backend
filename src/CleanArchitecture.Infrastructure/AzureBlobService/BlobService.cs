using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using CleanArchitecture.Application.DTOs.AzureBlob;
using CleanArchitecture.Application.ServiceContracts;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Infrastructure.AzureBlobService
{
  public class BlobService : IBlobService
  {
    private readonly BlobServiceClient _blobServiceClient;

    public BlobService(BlobServiceClient blobServiceClient)
    {
      _blobServiceClient = blobServiceClient;
    }

    public async Task<string> GetBlobsLinkAsync(string blobName)
    {
      var containerClient = _blobServiceClient.GetBlobContainerClient("defleur");
      var blobClient = containerClient.GetBlobClient(blobName);

      // Check if the BlobClient can generate a SAS URI
      if (blobClient.CanGenerateSasUri)
      {
        // Set the SAS token's properties (e.g., expiry time and permissions)
        var sasBuilder = new BlobSasBuilder
        {
          BlobContainerName = containerClient.Name,
          BlobName = blobClient.Name,
          Resource = "b", // 'b' indicates that the SAS is for a blob
          ExpiresOn = DateTimeOffset.UtcNow.AddHours(1) // SAS token valid for 1 hour
        };

        // Grant read permission
        sasBuilder.SetPermissions(BlobSasPermissions.Read);

        // Generate the SAS URI
        Uri sasUri = blobClient.GenerateSasUri(sasBuilder);
        return sasUri.AbsoluteUri;
      }

      // Fallback: return the blob's URL (won't be publicly accessible)
      return blobClient.Uri.AbsoluteUri;
    }

    public async Task<Stream> GetBlobAsync(string filePath)
    {
      var containerClient = _blobServiceClient.GetBlobContainerClient("defleur");
      var blobClient = containerClient.GetBlobClient(filePath);
      var blobDownloadInfo = await blobClient.DownloadAsync();

      return blobDownloadInfo.Value.Content;
    }

    /// <summary>
    /// This function upload the blob to the cloud (can also be used as updating)
    /// </summary>
    /// <param name="blobName">Name of file.</param>
    /// <param name="stream">File content</param>
    public async Task<List<string>> UploadBlobsAsync(IEnumerable<UploadRequest> uploadRequests)
    {
      var containerClient = _blobServiceClient.GetBlobContainerClient("defleur");
      List<string> url = new();
      foreach (var request in uploadRequests)
      {
        var blobClient = containerClient.GetBlobClient(request.FilePath);

        using (var stream = request.File.OpenReadStream())
        {
          // Option to make show file instead of downloading it when click the link
          var blobHttpHeader = new BlobHttpHeaders()
          {
            ContentType = request.File.ContentType,
            ContentDisposition = "inline"
          };
          await blobClient.UploadAsync(stream, new BlobUploadOptions()
          {
            HttpHeaders = blobHttpHeader
          });
          url.Add(blobClient.Uri.AbsoluteUri);
        }
      }

      return url;
    }

    public async Task<IEnumerable<string>> GetBlobsImageUrlsAsync(string filePath)
    {
      var containerClient = _blobServiceClient.GetBlobContainerClient("defleur");
      var blobUrls = new List<string>();

      // Use the cosmetic ID as a prefix to list only related images
      await foreach (var blobItem in containerClient.GetBlobsAsync(prefix: filePath))
      {
        var blobClient = containerClient.GetBlobClient(blobItem.Name);

        blobUrls.Add(blobClient.Uri.AbsoluteUri);
      }

      return blobUrls;
    }

    public async Task DeleteBlobAsync(string filePath)
    {
      var containerClient = _blobServiceClient.GetBlobContainerClient("defleur");
      var blobClient = containerClient.GetBlobClient(filePath);
      await blobClient.DeleteIfExistsAsync();
    }
  }
}