using Azure.Storage.Blobs.Models;
using CleanArchitecture.Application.DTOs.AzureBlob;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.ServiceContracts
{
  public interface IBlobService
  {
    Task<Stream> GetBlobAsync(string filePath);
    Task<List<string>> UploadBlobsAsync(IEnumerable<UploadRequest> uploadRequests);
    Task DeleteBlobAsync(string filePath);
  }
}