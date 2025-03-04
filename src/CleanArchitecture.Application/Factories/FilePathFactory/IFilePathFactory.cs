namespace CleanArchitecture.Application.Factories.FilePathFactory
{
  public interface IFilePathFactory
  {
    /// <summary>
    /// Get the directory structure for a specific object.
    /// </summary>
    /// <param name="objectType">Type of the object.</param>
    /// <param name="objectId">Object's identifier.</param>
    /// <param name="fileName">Name of the file, including the file's extension.</param>
    /// <returns></returns>
    string CreateFilePath(ObjectType objectType, Guid objectId, string fileName);
  }
}