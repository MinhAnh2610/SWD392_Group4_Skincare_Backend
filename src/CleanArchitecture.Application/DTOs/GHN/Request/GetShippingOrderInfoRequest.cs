using System.Text.Json.Serialization;

namespace CleanArchitecture.Application.DTOs.GHN.Request;

public class GetShippingOrderRequest
{
  [JsonPropertyName("order_code")]
  public string OrderCode { get; set; } = string.Empty;
}

public class SwitchShippingOrdersStatusRequest
{
  [JsonPropertyName("order_codes")]
  public List<string> OrderCodes { get; set; } = new List<string>();
}
