using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.ServiceContracts
{
  public interface IBlobService
  {
    Task<Stream> GetBlobAsync(string blobName);
    Task<string> UploadBlobsAsync(string blobName, IEnumerable<IFormFile> files);
    Task DeleteBlobAsync(string blobName);
  }
}