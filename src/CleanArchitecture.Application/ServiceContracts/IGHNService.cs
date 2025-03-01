namespace CleanArchitecture.Application.ServiceContracts;

public interface IGHNService
{
  Task<Result<string>> CreateReturnOrderAsync(object returnData);
  Task<Result<string>> CreateShippingOrderAsync(object shippingData);
  Task<Result<string>> GetOrderTrackingAsync(string orderCode);
  Task<Result<string>> GetShippingFeeAsync(object feeData);
  Task<Result<string>> GetStoreInformationAsync(object queryData);
  Task<Result<string>> GetDistrictAsync(int provinceId);
  Task<Result<string>> GetWardAsync(int districtId);
  Task<Result<string>> GetProvinceAsync();
}
