namespace CleanArchitecture.Application.Common
{
  public static class Message
  {
    public static string SUCCESSFUL_RETRIEVED(string objectName)
    {
      return $"Retrieved {objectName} successfully";
    } 
  }
}