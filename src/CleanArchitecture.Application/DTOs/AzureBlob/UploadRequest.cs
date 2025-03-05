using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.DTOs.AzureBlob
{
  public class UploadRequest
  {
    public UploadRequest(string filePath, IFormFile file)
    {
      FilePath = filePath;
      File = file;
    }
    public string FilePath { get; set; }
    public IFormFile File { get; set; }
  }
}