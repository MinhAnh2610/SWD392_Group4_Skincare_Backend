using System.Text.Json.Serialization;

namespace CleanArchitecture.Application.DTOs.GHN.Request;

public class CreateGHNOrderRequest
{
  [JsonPropertyName("payment_type_id")]
  public int PaymentTypeId { get; set; } // Map to Payment Type ID

  [JsonPropertyName("note")]
  public string Note { get; set; } = string.Empty; // Map to Note

  [JsonPropertyName("required_note")]
  public string RequiredNote { get; set; } = string.Empty; // Map to Required Note

  [JsonPropertyName("from_name")]
  public string FromName { get; set; } = string.Empty; // Map to From Name

  [JsonPropertyName("from_phone")]
  public string FromPhone { get; set; } = string.Empty; // Map to From Phone

  [JsonPropertyName("from_address")]
  public string FromAddress { get; set; } = string.Empty; // Map to From Address

  [JsonPropertyName("from_ward_name")]
  public string FromWardName { get; set; } = string.Empty; // Map to From Ward Name

  [JsonPropertyName("from_district_name")]
  public string FromDistrictName { get; set; } = string.Empty; // Map to From District Name

  [JsonPropertyName("from_province_name")]
  public string FromProvinceName { get; set; } = string.Empty; // Map to From Province Name

  [JsonPropertyName("return_phone")]
  public string ReturnPhone { get; set; } = string.Empty; // Map to Return Phone

  [JsonPropertyName("return_address")]
  public string ReturnAddress { get; set; } = string.Empty; // Map to Return Address

  [JsonPropertyName("return_district_id")]
  public int? ReturnDistrictId { get; set; } // Map to Return District ID (nullable)

  [JsonPropertyName("return_ward_code")]
  public string ReturnWardCode { get; set; } = string.Empty; // Map to Return Ward Code

  [JsonPropertyName("client_order_code")]
  public string ClientOrderCode { get; set; } = string.Empty; // Map to Client Order Code

  [JsonPropertyName("to_name")]
  public string ToName { get; set; } = string.Empty; // Map to To Name

  [JsonPropertyName("to_phone")]
  public string ToPhone { get; set; } = string.Empty; // Map to To Phone

  [JsonPropertyName("to_address")]
  public string ToAddress { get; set; } = string.Empty; // Map to To Address

  [JsonPropertyName("to_ward_code")]
  public string ToWardCode { get; set; } = string.Empty; // Map to To Ward Code

  [JsonPropertyName("to_district_id")]
  public int ToDistrictId { get; set; } // Map to To District ID

  [JsonPropertyName("cod_amount")]
  public int CodAmount { get; set; } // Map to COD Amount

  [JsonPropertyName("content")]
  public string Content { get; set; } = string.Empty; // Map to Content

  [JsonPropertyName("weight")]
  public int Weight { get; set; } // Map to Weight

  [JsonPropertyName("length")]
  public int Length { get; set; } // Map to Length

  [JsonPropertyName("width")]
  public int Width { get; set; } // Map to Width

  [JsonPropertyName("height")]
  public int Height { get; set; } // Map to Height

  [JsonPropertyName("pick_station_id")]
  public int PickStationId { get; set; } // Map to Pick Station ID

  [JsonPropertyName("deliver_station_id")]
  public int? DeliverStationId { get; set; } // Map to Deliver Station ID (nullable)

  [JsonPropertyName("insurance_value")]
  public int InsuranceValue { get; set; } // Map to Insurance Value

  [JsonPropertyName("service_id")]
  public int ServiceId { get; set; } // Map to Service ID

  [JsonPropertyName("service_type_id")]
  public int ServiceTypeId { get; set; } // Map to Service Type ID

  [JsonPropertyName("coupon")]
  public string Coupon { get; set; } = string.Empty; // Map to Coupon

  [JsonPropertyName("pick_shift")]
  public List<int> PickShift { get; set; } = new(); // Map to Pick Shift

  [JsonPropertyName("items")]
  public List<GHNOrderItem> Items { get; set; } = new(); // Map to Items
}

public class GHNOrderItem
{
  [JsonPropertyName("name")]
  public string Name { get; set; } = string.Empty; // Map to Item Name

  [JsonPropertyName("code")]
  public string Code { get; set; } = string.Empty; // Map to Item Code

  [JsonPropertyName("quantity")]
  public int Quantity { get; set; } // Map to Quantity

  [JsonPropertyName("price")]
  public int Price { get; set; } // Map to Price

  [JsonPropertyName("length")]
  public int Length { get; set; } // Map to Length

  [JsonPropertyName("width")]
  public int Width { get; set; } // Map to Width

  [JsonPropertyName("height")]
  public int Height { get; set; } // Map to Height

  [JsonPropertyName("weight")]
  public int Weight { get; set; } // Map to Weight
}
