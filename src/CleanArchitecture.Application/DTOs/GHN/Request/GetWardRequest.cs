using System.Text.Json.Serialization;

namespace CleanArchitecture.Application.DTOs.GHN.Request;

public class GetWardRequest
{
  [JsonPropertyName("district_id")]
  public int DistrictId { get; set; }
}
