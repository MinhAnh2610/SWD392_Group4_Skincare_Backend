using CleanArchitecture.Application.DTOs.GHN.Request;
using CleanArchitecture.Application.DTOs.GHN.Response;

namespace CleanArchitecture.Application.ServiceContracts;

public interface IGHNService
{
  Task<Result<List<ShippingOrderStatus>>> ChangeShippingOrderStatus(SwitchShippingOrdersStatusRequest request, string status);
  Task<Result<ShippingFeeData>> CreateShippingOrderAsync(CreateGHNOrderRequest request);
  Task<Result<ShippingOrderData>> GetOrderTrackingAsync(GetShippingOrderRequest request);
  Task<Result<FeeData>> GetShippingFeeAsync(CalculateShippingFeeRequest request);
  Task<Result<StoreData>> GetStoreInformationAsync();
  Task<Result<List<DistrictData>>> GetDistrictAsync(GetDistrictRequest request);
  Task<Result<List<WardData>>> GetWardAsync(GetWardRequest request);
  Task<Result<List<ProvinceData>>> GetProvinceAsync();
}
