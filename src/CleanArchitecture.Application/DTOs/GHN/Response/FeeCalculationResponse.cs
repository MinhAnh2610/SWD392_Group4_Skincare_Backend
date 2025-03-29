using System.Text.Json.Serialization;

namespace CleanArchitecture.Application.DTOs.GHN.Response;

public class FeeData
{
  [JsonPropertyName("total")]
  public int Total { get; set; } // Map to Total

  [JsonPropertyName("service_fee")]
  public int ServiceFee { get; set; } // Map to Service Fee

  [JsonPropertyName("insurance_fee")]
  public int InsuranceFee { get; set; } // Map to Insurance Fee

  [JsonPropertyName("pick_station_fee")]
  public int PickStationFee { get; set; } // Map to Pick Station Fee

  [JsonPropertyName("coupon_value")]
  public int CouponValue { get; set; } // Map to Coupon Value

  [JsonPropertyName("r2s_fee")]
  public int R2SFee { get; set; } // Map to R2S Fee

  [JsonPropertyName("document_return")]
  public int DocumentReturn { get; set; } // Map to Document Return

  [JsonPropertyName("double_check")]
  public int DoubleCheck { get; set; } // Map to Double Check

  [JsonPropertyName("cod_fee")]
  public int CodFee { get; set; } // Map to COD Fee

  [JsonPropertyName("pick_remote_areas_fee")]
  public int PickRemoteAreasFee { get; set; } // Map to Pick Remote Areas Fee

  [JsonPropertyName("deliver_remote_areas_fee")]
  public int DeliverRemoteAreasFee { get; set; } // Map to Deliver Remote Areas Fee

  [JsonPropertyName("cod_failed_fee")]
  public int CodFailedFee { get; set; } // Map to COD Failed Fee
}