using System.Text.Json.Serialization;

namespace CleanArchitecture.Application.DTOs.GHN.Response;

public class ShippingOrderData
{
  [JsonPropertyName("shop_id")]
  public int ShopId { get; set; } // Map to Shop ID

  [JsonPropertyName("client_id")]
  public int ClientId { get; set; } // Map to Client ID

  [JsonPropertyName("return_name")]
  public string ReturnName { get; set; } = string.Empty; // Map to Return Name

  [JsonPropertyName("return_phone")]
  public string ReturnPhone { get; set; } = string.Empty; // Map to Return Phone

  [JsonPropertyName("return_address")]
  public string ReturnAddress { get; set; } = string.Empty; // Map to Return Address

  [JsonPropertyName("return_ward_code")]
  public string ReturnWardCode { get; set; } = string.Empty; // Map to Return Ward Code

  [JsonPropertyName("return_district_id")]
  public int ReturnDistrictId { get; set; } // Map to Return District ID

  [JsonPropertyName("from_name")]
  public string FromName { get; set; } = string.Empty; // Map to From Name

  [JsonPropertyName("from_phone")]
  public string FromPhone { get; set; } = string.Empty; // Map to From Phone

  [JsonPropertyName("from_address")]
  public string FromAddress { get; set; } = string.Empty; // Map to From Address

  [JsonPropertyName("from_ward_code")]
  public string FromWardCode { get; set; } = string.Empty; // Map to From Ward Code

  [JsonPropertyName("from_district_id")]
  public int FromDistrictId { get; set; } // Map to From District ID

  [JsonPropertyName("deliver_station_id")]
  public int DeliverStationId { get; set; } // Map to Deliver Station ID

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

  [JsonPropertyName("weight")]
  public int Weight { get; set; } // Map to Weight

  [JsonPropertyName("length")]
  public int Length { get; set; } // Map to Length

  [JsonPropertyName("width")]
  public int Width { get; set; } // Map to Width

  [JsonPropertyName("height")]
  public int Height { get; set; } // Map to Height

  [JsonPropertyName("converted_weight")]
  public int ConvertedWeight { get; set; } // Map to Converted Weight

  [JsonPropertyName("service_type_id")]
  public int ServiceTypeId { get; set; } // Map to Service Type ID

  [JsonPropertyName("service_id")]
  public int ServiceId { get; set; } // Map to Service ID

  [JsonPropertyName("payment_type_id")]
  public int PaymentTypeId { get; set; } // Map to Payment Type ID

  [JsonPropertyName("custom_service_fee")]
  public int CustomServiceFee { get; set; } // Map to Custom Service Fee

  [JsonPropertyName("cod_amount")]
  public int CodAmount { get; set; } // Map to COD Amount

  [JsonPropertyName("cod_collect_date")]
  public DateTime? CodCollectDate { get; set; } // Map to COD Collect Date (nullable)

  [JsonPropertyName("cod_transfer_date")]
  public DateTime? CodTransferDate { get; set; } // Map to COD Transfer Date (nullable)

  [JsonPropertyName("is_cod_transferred")]
  public bool IsCodTransferred { get; set; } // Map to Is COD Transferred

  [JsonPropertyName("is_cod_collected")]
  public bool IsCodCollected { get; set; } // Map to Is COD Collected

  [JsonPropertyName("insurance_value")]
  public int InsuranceValue { get; set; } // Map to Insurance Value

  [JsonPropertyName("order_value")]
  public int OrderValue { get; set; } // Map to Order Value

  [JsonPropertyName("pick_station_id")]
  public int PickStationId { get; set; } // Map to Pick Station ID

  [JsonPropertyName("client_order_code")]
  public string ClientOrderCode { get; set; } = string.Empty; // Map to Client Order Code

  [JsonPropertyName("cod_failed_amount")]
  public int CodFailedAmount { get; set; } // Map to COD Failed Amount

  [JsonPropertyName("cod_failed_collect_date")]
  public string CodFailedCollectDate { get; set; } = string.Empty; // Map to COD Failed Collect Date

  [JsonPropertyName("required_note")]
  public string RequiredNote { get; set; } = string.Empty; // Map to Required Note

  [JsonPropertyName("content")]
  public string Content { get; set; } = string.Empty; // Map to Content

  [JsonPropertyName("note")]
  public string Note { get; set; } = string.Empty; // Map to Note

  [JsonPropertyName("employee_note")]
  public string EmployeeNote { get; set; } = string.Empty; // Map to Employee Note

  [JsonPropertyName("coupon")]
  public string Coupon { get; set; } = string.Empty; // Map to Coupon

  [JsonPropertyName("_id")]
  public string Id { get; set; } = string.Empty; // Map to Order ID

  [JsonPropertyName("order_code")]
  public string OrderCode { get; set; } = string.Empty; // Map to Order Code

  [JsonPropertyName("version_no")]
  public string VersionNo { get; set; } = string.Empty; // Map to Version No

  [JsonPropertyName("updated_ip")]
  public string UpdatedIp { get; set; } = string.Empty; // Map to Updated IP

  [JsonPropertyName("updated_employee")]
  public int UpdatedEmployee { get; set; } // Map to Updated Employee

  [JsonPropertyName("updated_client")]
  public int UpdatedClient { get; set; } // Map to Updated Client

  [JsonPropertyName("updated_source")]
  public string UpdatedSource { get; set; } = string.Empty; // Map to Updated Source

  [JsonPropertyName("updated_date")]
  public DateTime UpdatedDate { get; set; } // Map to Updated Date

  [JsonPropertyName("updated_warehouse")]
  public int UpdatedWarehouse { get; set; } // Map to Updated Warehouse

  [JsonPropertyName("created_ip")]
  public string CreatedIp { get; set; } = string.Empty; // Map to Created IP

  [JsonPropertyName("created_employee")]
  public int CreatedEmployee { get; set; } // Map to Created Employee

  [JsonPropertyName("created_client")]
  public int CreatedClient { get; set; } // Map to Created Client

  [JsonPropertyName("created_source")]
  public string CreatedSource { get; set; } = string.Empty; // Map to Created Source

  [JsonPropertyName("created_date")]
  public DateTime CreatedDate { get; set; } // Map to Created Date

  [JsonPropertyName("status")]
  public string Status { get; set; } = string.Empty; // Map to Status

  [JsonPropertyName("pick_warehouse_id")]
  public int PickWarehouseId { get; set; } // Map to Pick Warehouse ID

  [JsonPropertyName("deliver_warehouse_id")]
  public int DeliverWarehouseId { get; set; } // Map to Deliver Warehouse ID

  [JsonPropertyName("current_warehouse_id")]
  public int CurrentWarehouseId { get; set; } // Map to Current Warehouse ID

  [JsonPropertyName("return_warehouse_id")]
  public int ReturnWarehouseId { get; set; } // Map to Return Warehouse ID

  [JsonPropertyName("next_warehouse_id")]
  public int NextWarehouseId { get; set; } // Map to Next Warehouse ID

  [JsonPropertyName("leadtime")]
  public DateTime Leadtime { get; set; } // Map to Leadtime

  [JsonPropertyName("order_date")]
  public DateTime OrderDate { get; set; } // Map to Order Date

  [JsonPropertyName("soc_id")]
  public string SocId { get; set; } = string.Empty; // Map to SOC ID

  [JsonPropertyName("finish_date")]
  public string FinishDate { get; set; } = string.Empty; // Map to Finish Date

  [JsonPropertyName("tag")]
  public List<string> Tag { get; set; } = new(); // Map to Tag

  [JsonPropertyName("log")]
  public List<ShippingOrderLog> Log { get; set; } = new(); // Map to Log
}

public class ShippingOrderLog
{
  [JsonPropertyName("status")]
  public string Status { get; set; } = string.Empty; // Map to Log Status

  [JsonPropertyName("updated_date")]
  public DateTime UpdatedDate { get; set; } // Map to Log Updated Date
}