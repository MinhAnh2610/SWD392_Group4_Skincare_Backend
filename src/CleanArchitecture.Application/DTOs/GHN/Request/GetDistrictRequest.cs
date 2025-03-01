using System.Text.Json.Serialization;

namespace CleanArchitecture.Application.DTOs.GHN.Request;

public class GetDistrictRequest
{
  [JsonPropertyName("province_id")]
  public int ProvinceId { get; set; }
}
