namespace CleanArchitecture.Application.DTOs.VnPay
{
  public class VnPayPaymentResponseDto
  {
    public bool Success { get; set; }
    public string? OrderDescription { get; set; }
    public string TransactionId { get; set; }
    public string? TransactionOrderId { get; set; }
    public string? PaymentMethod { get; set; }
    public string? PaymentId { get; set; }
    public string? TotalAmount { get; set; } // NEW property
    public string? Token { get; set; }
    public string? ResponseCode { get; set; }
  }
}
