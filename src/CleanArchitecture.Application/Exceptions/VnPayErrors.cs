namespace CleanArchitecture.Application.Exceptions
{
  public static class VnPayErrors
  {
    public static readonly Error SignatureValidationFailed =
        new Error("VnPay.SignatureValidationFailed", "VNPay signature validation failed.");

    public static readonly Error InvalidOrderId =
        new Error("VnPay.InvalidOrderId", "Invalid Order Id in VNPay response.");

    public static readonly Error PaymentFailed =
        new Error("VnPay.PaymentFailed", "VNPay reported a payment failure.");

    // You can add more VNPay-specific errors as needed.
  }
}
