using System.Text.Json.Serialization;

namespace CleanArchitecture.Application.DTOs.GHN.Request;

public class CalculateShippingFeeRequest
{
  [JsonPropertyName("from_district_id")]
  public int FromDistrictId { get; set; } // Map to From District ID

  [JsonPropertyName("from_ward_code")]
  public string FromWardCode { get; set; } = string.Empty; // Map to From Ward Code

  [JsonPropertyName("service_id")]
  public int ServiceId { get; set; } // Map to Service ID

  [JsonPropertyName("service_type_id")]
  public int? ServiceTypeId { get; set; } // Map to Service Type ID (nullable)

  [JsonPropertyName("to_district_id")]
  public int ToDistrictId { get; set; } // Map to To District ID

  [JsonPropertyName("to_ward_code")]
  public string ToWardCode { get; set; } = string.Empty; // Map to To Ward Code

  [JsonPropertyName("height")]
  public int Height { get; set; } // Map to Height

  [JsonPropertyName("length")]
  public int Length { get; set; } // Map to Length

  [JsonPropertyName("weight")]
  public int Weight { get; set; } // Map to Weight

  [JsonPropertyName("width")]
  public int Width { get; set; } // Map to Width

  [JsonPropertyName("insurance_value")]
  public int InsuranceValue { get; set; } // Map to Insurance Value

  [JsonPropertyName("cod_failed_amount")]
  public int CodFailedAmount { get; set; } // Map to COD Failed Amount

  [JsonPropertyName("coupon")]
  public string Coupon { get; set; } = string.Empty; // Map to Coupon

  [JsonPropertyName("items")]
  public List<GHNOrderItem> Items { get; set; } = new(); // Map to Items
}