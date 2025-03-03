namespace CleanArchitecture.Application.Constants.FirebasePath
{
  public static class ImagePath
  {
    public static string GetCosmeticThumbnailPath(Guid cosmeticId)
    {
      return $"/cosmetics/{cosmeticId}/images/thumbnail";
    }

    public static string GetCosmeticImagesPath(Guid cosmeticId)
    {
      return $"/cosmetics/{cosmeticId}/images";
    }
  }
}