using System.Text.Json.Serialization;

namespace CleanArchitecture.Application.DTOs.GHN.Response;

public class DistrictData
{
  [JsonPropertyName("DistrictID")]
  public int DistrictId { get; set; } // Map to DistrictID

  [JsonPropertyName("ProvinceID")]
  public int ProvinceId { get; set; } // Map to ProvinceID

  [JsonPropertyName("DistrictName")]
  public string DistrictName { get; set; } = string.Empty; // Map to DistrictName

  [JsonPropertyName("Code")]
  public string Code { get; set; } = string.Empty; // Map to Code

  [JsonPropertyName("Type")]
  public int Type { get; set; } // Map to Type

  [JsonPropertyName("SupportType")]
  public int SupportType { get; set; } // Map to SupportType
}
