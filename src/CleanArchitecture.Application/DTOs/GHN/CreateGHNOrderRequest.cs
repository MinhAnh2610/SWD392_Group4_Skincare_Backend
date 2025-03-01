namespace CleanArchitecture.Application.DTOs.GHN;

public class CreateGHNOrderRequest
{
  public int PaymentTypeId { get; set; }
  public string? Note { get; set; }
  public required string RequiredNote { get; set; }
  public required string FromName { get; set; }
  public required string FromPhone { get; set; }
  public required string FromAddress { get; set; }
  public required string FromWardName { get; set; }
  public required string FromDistrictName { get; set; }
  public required string FromProvinceName { get; set; }
  public string? ReturnPhone { get; set; }
  public string? ReturnAddress { get; set; }
  public int? ReturnDistrictId { get; set; }
  public string? ReturnWardCode { get; set; }
  public string? ClientOrderCode { get; set; }
  public required string ToName { get; set; }
  public required string ToPhone { get; set; }
  public required string ToAddress { get; set; }
  public required string ToWardCode { get; set; }
  public int ToDistrictId { get; set; }
  public decimal CodAmount { get; set; }
  public string? Content { get; set; }
  public int Weight { get; set; }
  public int Length { get; set; }
  public int Width { get; set; }
  public int Height { get; set; }
  public int? DeliverStationId { get; set; }
  public int ServiceId { get; set; }
  public int ServiceTypeId { get; set; }
  public List<GHNOrderItemRequest> Items { get; set; } = new List<GHNOrderItemRequest>();
}
