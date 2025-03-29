using System.Text.Json.Serialization;

namespace CleanArchitecture.Application.DTOs.GHN.Response;

public class StoreData
{
  [JsonPropertyName("last_offset")]
  public int LastOffset { get; set; }

  [JsonPropertyName("shops")]
  public List<Shop> Shops { get; set; } = new();
}

public class Shop
{
  [JsonPropertyName("_id")]
  public int Id { get; set; }

  [JsonPropertyName("name")]
  public string Name { get; set; } = string.Empty;

  [JsonPropertyName("phone")]
  public string Phone { get; set; } = string.Empty;

  [JsonPropertyName("address")]
  public string Address { get; set; } = string.Empty;

  [JsonPropertyName("ward_code")]
  public string WardCode { get; set; } = string.Empty;

  [JsonPropertyName("district_id")]
  public int DistrictId { get; set; }

  [JsonPropertyName("client_id")]
  public int ClientId { get; set; }

  [JsonPropertyName("bank_account_id")]
  public int BankAccountId { get; set; }

  [JsonPropertyName("status")]
  public int Status { get; set; }

  [JsonPropertyName("location")]
  public Dictionary<string, object> Location { get; set; } = new();

  [JsonPropertyName("version_no")]
  public string VersionNo { get; set; } = string.Empty;

  [JsonPropertyName("is_created_chat_channel")]
  public bool IsCreatedChatChannel { get; set; }

  [JsonPropertyName("updated_ip")]
  public string UpdatedIp { get; set; } = string.Empty;

  [JsonPropertyName("updated_employee")]
  public int UpdatedEmployee { get; set; }

  [JsonPropertyName("updated_client")]
  public int UpdatedClient { get; set; }

  [JsonPropertyName("updated_source")]
  public string UpdatedSource { get; set; } = string.Empty;

  [JsonPropertyName("updated_date")]
  public DateTime UpdatedDate { get; set; }

  [JsonPropertyName("created_ip")]
  public string CreatedIp { get; set; } = string.Empty;

  [JsonPropertyName("created_employee")]
  public int CreatedEmployee { get; set; }

  [JsonPropertyName("created_client")]
  public int CreatedClient { get; set; }

  [JsonPropertyName("created_source")]
  public string CreatedSource { get; set; } = string.Empty;

  [JsonPropertyName("created_date")]
  public DateTime CreatedDate { get; set; }
}
