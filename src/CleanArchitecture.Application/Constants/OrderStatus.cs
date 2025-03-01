namespace CleanArchitecture.Application.Constants;

public static class OrderStatus
{
  public const string PENDING = "PENDING_PAYMENT";
  public const string CONFIRMED = "CONFIRMED";
  public const string PROCESSING = "PROCESSING";
  public const string DELIVERY = "DELIVERY";
  public const string COMPLETED = "COMPLETED";
  public const string REFUNDED = "REFUNDED";
  public const string CANCELLED = "CANCELLED";
  public const string FAILED = "PAYMENT_FAILED";
  public const string EXPIRED = "EXPIRED";
}
