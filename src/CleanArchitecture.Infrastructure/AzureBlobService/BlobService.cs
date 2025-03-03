using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
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

    public async Task<Stream> GetBlobAsync(string blobName)
    {
      var containerClient = _blobServiceClient.GetBlobContainerClient("defleur");
      var blobClient = containerClient.GetBlobClient(blobName);
      var blobDownloadInfo = await blobClient.DownloadAsync();

      return blobDownloadInfo.Value.Content;
    }

    /// <summary>
    /// This function upload the blob to the cloud (can also be used as updating)
    /// </summary>
    /// <param name="blobName">Name of file.</param>
    /// <param name="stream">File content</param>
    public async Task<string> UploadBlobsAsync(string blobName, IEnumerable<IFormFile> files)
    {
      var containerClient = _blobServiceClient.GetBlobContainerClient("defleur");
      string url = string.Empty;
      foreach (var file in files)
      {
        blobName = $"{blobName}/{Guid.NewGuid()}_{file.FileName}";
        var blobClient = containerClient.GetBlobClient(blobName);

        using (var stream = file.OpenReadStream())
        {
          // Option to make show file instead of downloading it when click the link
          var blobHttpHeader = new BlobHttpHeaders()
          {
            ContentType = file.ContentType,
            ContentDisposition = "inline"
          };
          await blobClient.UploadAsync(stream, new BlobUploadOptions()
          {
            HttpHeaders = blobHttpHeader
          });
          url = blobClient.Uri.AbsoluteUri;
        }
      }

      return url;
    }

    public async Task<IEnumerable<string>> GetBlobsImageUrlsAsync(string blobName)
    {
      var containerClient = _blobServiceClient.GetBlobContainerClient("defleur");
      var blobUrls = new List<string>();

      // Use the cosmetic ID as a prefix to list only related images
      await foreach (var blobItem in containerClient.GetBlobsAsync(prefix: blobName + "/"))
      {
        var blobClient = containerClient.GetBlobClient(blobItem.Name);

        blobUrls.Add(blobClient.Uri.AbsoluteUri);
      }

      return blobUrls;
    }

    public async Task DeleteBlobAsync(string blobName)
    {
      var containerClient = _blobServiceClient.GetBlobContainerClient("defleur");
      var blobClient = containerClient.GetBlobClient(blobName);
      await blobClient.DeleteIfExistsAsync();
    }
  }
}