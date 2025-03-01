namespace CleanArchitecture.Application.Services;

using CleanArchitecture.Application.DTOs.GHN.Request;
using CleanArchitecture.Application.DTOs.GHN.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class GHNService : IGHNService
{
  private readonly HttpClient _httpClient;
  private readonly string _baseUrl;
  private readonly string _token;
  private readonly string _shopId;

  public GHNService(HttpClient httpClient, IConfiguration config)
  {
    _httpClient = httpClient;
    _baseUrl = config["GHN:BaseUrl"]!;
    _token = config["GHN:Token"]!;
    _shopId = config["GHN:ShopId"]!;
  }

  private HttpRequestMessage CreateRequest(HttpMethod method, string endpoint, object body = null)
  {
    var request = new HttpRequestMessage(method, $"{_baseUrl}/{endpoint}");
    request.Headers.Add("ShopId", _shopId);
    request.Headers.Add("Token", _token);
    if (body != null)
    {
      string json = JsonSerializer.Serialize(body);
      request.Content = new StringContent(json, Encoding.UTF8, "application/json");
    }
    return request;
  }

  private async Task<Result<T>> SendRequestAsync<T>(HttpRequestMessage request)
  {
    try
    {
      var response = await _httpClient.SendAsync(request);
      string content = await response.Content.ReadAsStringAsync();

      var deserializedData = JsonSerializer.Deserialize<GHNResponse<T>>(content, new JsonSerializerOptions
      {
        PropertyNameCaseInsensitive = true
      });

      if (!response.IsSuccessStatusCode)
      {
        return Result<T>.Failure(new List<Error>
        {
          new Error(deserializedData!.Code.ToString(), deserializedData.Message)
        }, StatusCodes.Status400BadRequest);
      }

      return Result<T>.Success(deserializedData!.Data!, StatusCodes.Status200OK);
    }
    catch (Exception ex)
    {
      return Result<T>.Failure(new List<Error>
      {
        new Error(ex.Message, ex.StackTrace!)
      }, StatusCodes.Status400BadRequest);
    }
  }

  public Task<Result<ShippingFeeData>> CreateShippingOrderAsync(CreateGHNOrderRequest request) =>
      SendRequestAsync<ShippingFeeData>(CreateRequest(HttpMethod.Post, "v2/shipping-order/create", request));

  public Task<Result<object>> GetShippingFeeAsync(object feeData) =>
      SendRequestAsync<object>(CreateRequest(HttpMethod.Post, "v2/shipping-order/fee", feeData));

  public Task<Result<object>> GetOrderTrackingAsync(string orderCode) =>
      SendRequestAsync<object>(CreateRequest(HttpMethod.Get, $"v2/shipping-order/detail?order_code={orderCode}"));

  public Task<Result<object>> CreateReturnOrderAsync(object returnData) =>
      SendRequestAsync<object>(CreateRequest(HttpMethod.Post, "v2/shipping-order/create", returnData));

  public Task<Result<List<StoreData>>> GetStoreInformationAsync(object queryData) =>
      SendRequestAsync<List<StoreData>>(CreateRequest(HttpMethod.Post, "v2/shop/all", queryData));

  public Task<Result<List<DistrictData>>> GetDistrictAsync(GetDistrictRequest request) =>
      SendRequestAsync<List<DistrictData>>(CreateRequest(HttpMethod.Post, "master-data/district", request));

  public Task<Result<List<WardData>>> GetWardAsync(GetWardRequest request) =>
      SendRequestAsync<List<WardData>>(CreateRequest(HttpMethod.Post, "master-data/ward?district_id", request));

  public Task<Result<List<ProvinceData>>> GetProvinceAsync() =>
      SendRequestAsync<List<ProvinceData>>(CreateRequest(HttpMethod.Get, "master-data/province"));
}
