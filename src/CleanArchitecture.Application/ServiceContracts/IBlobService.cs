using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.ServiceContracts
{
  public interface IBlobService
  {
    Task<Stream> GetBlobAsync(string filePath);
    Task<string> UploadBlobsAsync(string filePath, IEnumerable<IFormFile> files);
    Task DeleteBlobAsync(string filePath);
  }
}