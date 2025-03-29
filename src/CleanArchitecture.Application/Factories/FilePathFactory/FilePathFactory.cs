namespace CleanArchitecture.Application.Factories.FilePathFactory
{
  public class FilePathFactory : IFilePathFactory
  {
    public string CreateFilePath(ObjectType objectType, Guid objectId, string fileName)
    {
      return objectType switch
      {
        ObjectType.CosmeticThumbnail => $"Cosmetics/{objectId.ToString()}/Thumbnail/{Guid.NewGuid()}_{fileName}",
        ObjectType.CosmeticImage => $"Cosmetics/{objectId.ToString()}/{Guid.NewGuid()}_{fileName}",
        ObjectType.BrandLogo => $"Brands/{objectId}/Logo/{Guid.NewGuid()}_{fileName}",
        ObjectType.BlogThumbnail => $"Blogs/{objectId.ToString()}/Thumbnail/{Guid.NewGuid()}_{fileName}",
        _ => string.Empty
      };
    }
  }
}