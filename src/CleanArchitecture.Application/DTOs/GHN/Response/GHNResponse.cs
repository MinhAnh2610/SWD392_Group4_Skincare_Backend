using System.Text.Json.Serialization;

namespace CleanArchitecture.Application.DTOs.GHN.Response;

public class GHNResponse<T>
{
  [JsonPropertyName("code")]
  public int Code { get; set; } // Map to Status Code

  [JsonPropertyName("message")]
  public string Message { get; set; } = string.Empty; // Map to Message

  [JsonPropertyName("data")]
  public T? Data { get; set; } = default!; // Map to Data
}
