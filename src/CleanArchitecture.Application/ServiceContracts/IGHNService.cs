namespace CleanArchitecture.Application.ServiceContracts;

public interface IGHNService
{
  Task<Result<string>> CreateReturnOrderAsync(object returnData);
  Task<Result<string>> CreateShippingOrderAsync(object shippingData);
  Task<Result<string>> GetOrderTrackingAsync(string orderCode);
  Task<Result<string>> GetShippingFeeAsync(object feeData);
}
