using CleanArchitecture.Application.DTOs.GHN.Request;
using CleanArchitecture.Application.DTOs.GHN.Response;

namespace CleanArchitecture.Application.ServiceContracts;

public interface IGHNService
{
  Task<Result<object>> CreateReturnOrderAsync(object returnData);
  Task<Result<ShippingFeeData>> CreateShippingOrderAsync(CreateGHNOrderRequest request);
  Task<Result<object>> GetOrderTrackingAsync(string orderCode);
  Task<Result<object>> GetShippingFeeAsync(object feeData);
  Task<Result<List<StoreData>>> GetStoreInformationAsync(object queryData);
  Task<Result<List<DistrictData>>> GetDistrictAsync(GetDistrictRequest request);
  Task<Result<List<WardData>>> GetWardAsync(GetWardRequest request);
  Task<Result<List<ProvinceData>>> GetProvinceAsync();
}
