using System.Text.Json.Serialization;

namespace CleanArchitecture.Application.DTOs.GHN.Response;

public class ProvinceData
{
  [JsonPropertyName("ProvinceID")]
  public int ProvinceId { get; set; } // Map to ProvinceID

  [JsonPropertyName("ProvinceName")]
  public string ProvinceName { get; set; } = string.Empty; // Map to ProvinceName

  [JsonPropertyName("CountryID")]
  public int CountryId { get; set; } // Map to CountryID

  [JsonPropertyName("Code")]
  public string Code { get; set; } = string.Empty; // Map to Code

  [JsonPropertyName("NameExtension")]
  public List<string> NameExtension { get; set; } = new(); // Map to NameExtension

  [JsonPropertyName("IsEnable")]
  public int IsEnable { get; set; } // Map to IsEnable

  [JsonPropertyName("RegionID")]
  public int RegionId { get; set; } // Map to RegionID

  [JsonPropertyName("RegionCPN")]
  public int RegionCPN { get; set; } // Map to RegionCPN

  [JsonPropertyName("UpdatedBy")]
  public int UpdatedBy { get; set; } // Map to UpdatedBy

  [JsonPropertyName("CreatedAt")]
  public string CreatedAt { get; set; } = string.Empty; // Map to CreatedAt

  [JsonPropertyName("UpdatedAt")]
  public string UpdatedAt { get; set; } = string.Empty; // Map to UpdatedAt

  [JsonPropertyName("AreaID")]
  public int AreaId { get; set; } // Map to AreaID

  [JsonPropertyName("CanUpdateCOD")]
  public bool CanUpdateCOD { get; set; } // Map to CanUpdateCOD

  [JsonPropertyName("Status")]
  public int Status { get; set; } // Map to Status

  [JsonPropertyName("CreatedIP")]
  public string CreatedIP { get; set; } = string.Empty; // Map to CreatedIP

  [JsonPropertyName("CreatedEmployee")]
  public int CreatedEmployee { get; set; } // Map to CreatedEmployee

  [JsonPropertyName("CreatedSource")]
  public string CreatedSource { get; set; } = string.Empty; // Map to CreatedSource

  [JsonPropertyName("CreatedDate")]
  public DateTime CreatedDate { get; set; } // Map to CreatedDate

  [JsonPropertyName("UpdatedEmployee")]
  public int UpdatedEmployee { get; set; } // Map to UpdatedEmployee

  [JsonPropertyName("UpdatedSource")]
  public string UpdatedSource { get; set; } = string.Empty; // Map to UpdatedSource

  [JsonPropertyName("UpdatedDate")]
  public DateTime UpdatedDate { get; set; } // Map to UpdatedDate
}
