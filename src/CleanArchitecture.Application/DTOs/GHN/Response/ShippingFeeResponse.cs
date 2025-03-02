using System.Text.Json.Serialization;

namespace CleanArchitecture.Application.DTOs.GHN.Response;

public class ShippingFeeData
{
  [JsonPropertyName("district_encode")]
  public string DistrictEncode { get; set; } = string.Empty; // Map to District Encode

  [JsonPropertyName("expected_delivery_time")]
  public DateTime ExpectedDeliveryTime { get; set; } // Map to Expected Delivery Time

  [JsonPropertyName("fee")]
  public FeeDetails Fee { get; set; } = new(); // Map to Fee

  [JsonPropertyName("order_code")]
  public string OrderCode { get; set; } = string.Empty; // Map to Order Code

  [JsonPropertyName("sort_code")]
  public string SortCode { get; set; } = string.Empty; // Map to Sort Code

  [JsonPropertyName("total_fee")]
  public int TotalFee { get; set; } // Map to Total Fee

  [JsonPropertyName("trans_type")]
  public string TransType { get; set; } = string.Empty; // Map to Transport Type

  [JsonPropertyName("ward_encode")]
  public string WardEncode { get; set; } = string.Empty; // Map to Ward Encode
}

public class FeeDetails
{
  [JsonPropertyName("coupon")]
  public int Coupon { get; set; } // Map to Coupon

  [JsonPropertyName("insurance")]
  public int Insurance { get; set; } // Map to Insurance

  [JsonPropertyName("main_service")]
  public int MainService { get; set; } // Map to Main Service

  [JsonPropertyName("r2s")]
  public int R2S { get; set; } // Map to R2S

  [JsonPropertyName("return")]
  public int Return { get; set; } // Map to Return

  [JsonPropertyName("station_do")]
  public int StationDo { get; set; } // Map to Station DO

  [JsonPropertyName("station_pu")]
  public int StationPu { get; set; } // Map to Station PU
}
