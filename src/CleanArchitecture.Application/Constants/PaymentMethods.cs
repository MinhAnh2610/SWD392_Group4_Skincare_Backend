namespace CleanArchitecture.Application.Constants;

public static class PaymentMethods
{
  public const string ONLINE = "ONLINE";
  public const string COD = "COD";
  public const string CASH = "CASH";

  public static IEnumerable<string> ListPaymentMethods => ["ONLINE", "COD", "CASH"];
}
