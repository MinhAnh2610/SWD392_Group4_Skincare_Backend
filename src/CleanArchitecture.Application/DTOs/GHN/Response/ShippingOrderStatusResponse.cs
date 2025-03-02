using System.Text.Json.Serialization;

namespace CleanArchitecture.Application.DTOs.GHN.Response;

public class ShippingOrderStatus
{
  [JsonPropertyName("order_code")]
  public string OrderCode { get; set; } = string.Empty; // Map to Order Code

  [JsonPropertyName("result")]
  public bool Result { get; set; } // Map to Result

  [JsonPropertyName("message")]
  public string Message { get; set; } = string.Empty; // Map to Message
}
