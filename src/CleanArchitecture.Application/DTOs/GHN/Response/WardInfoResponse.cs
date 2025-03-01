using System.Text.Json.Serialization;

namespace CleanArchitecture.Application.DTOs.GHN.Response;

public class WardData
{
  [JsonPropertyName("WardCode")]
  public string WardCode { get; set; } = string.Empty; // Map to WardCode

  [JsonPropertyName("DistrictID")]
  public int DistrictId { get; set; } // Map to DistrictID

  [JsonPropertyName("WardName")]
  public string WardName { get; set; } = string.Empty; // Map to WardName

  [JsonPropertyName("NameExtension")]
  public List<string> NameExtension { get; set; } = new(); // Map to NameExtension

  [JsonPropertyName("CanUpdateCOD")]
  public bool CanUpdateCOD { get; set; } // Map to CanUpdateCOD

  [JsonPropertyName("SupportType")]
  public int SupportType { get; set; } // Map to SupportType

  [JsonPropertyName("PickType")]
  public int PickType { get; set; } // Map to PickType

  [JsonPropertyName("DeliverType")]
  public int DeliverType { get; set; } // Map to DeliverType

  [JsonPropertyName("WhiteListClient")]
  public WhiteListClient WhiteListClient { get; set; } = new(); // Map to WhiteListClient

  [JsonPropertyName("WhiteListWard")]
  public WhiteListWard WhiteListWard { get; set; } = new(); // Map to WhiteListWard

  [JsonPropertyName("Status")]
  public int Status { get; set; } // Map to Status

  [JsonPropertyName("ReasonCode")]
  public string ReasonCode { get; set; } = string.Empty; // Map to ReasonCode

  [JsonPropertyName("ReasonMessage")]
  public string ReasonMessage { get; set; } = string.Empty; // Map to ReasonMessage

  [JsonPropertyName("OnDates")]
  public List<DateTime> OnDates { get; set; } = new(); // Map to OnDates

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

public class WhiteListClient
{
  [JsonPropertyName("From")]
  public List<string> From { get; set; } = new(); // Map to From

  [JsonPropertyName("To")]
  public List<string> To { get; set; } = new(); // Map to To

  [JsonPropertyName("Return")]
  public List<string> Return { get; set; } = new(); // Map to Return
}

public class WhiteListWard
{
  [JsonPropertyName("From")]
  public object? From { get; set; } // Map to From (can be null or an object)

  [JsonPropertyName("To")]
  public object? To { get; set; } // Map to To (can be null or an object)
}
