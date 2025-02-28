namespace CleanArchitecture.Application.Services;

using CleanArchitecture.Application.Interfaces;
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
  private readonly int _shopId;

  public GHNService(HttpClient httpClient, IConfiguration config)
  {
    _httpClient = httpClient;
    _baseUrl = config["GHN:BaseUrl"]!;
    _token = config["GHN:Token"]!;
    _shopId = int.Parse(config["GHN:ShopId"]!);
  }

  private HttpRequestMessage CreateRequest(HttpMethod method, string endpoint, object body = null)
  {
    var request = new HttpRequestMessage(method, $"{_baseUrl}/{endpoint}");
    request.Headers.Add("Token", _token);
    if (body != null)
    {
      string json = JsonSerializer.Serialize(body);
      request.Content = new StringContent(json, Encoding.UTF8, "application/json");
    }
    return request;
  }

  private async Task<Result<string>> SendRequestAsync(HttpRequestMessage request)
  {
    try
    {
      var response = await _httpClient.SendAsync(request);
      string content = await response.Content.ReadAsStringAsync();

      if (!response.IsSuccessStatusCode)
      {
        return Result<string>.Failure(new List<Error>
        {
          new Error(response.Content.ToString()!, response.RequestMessage!.ToString())
        }, int.Parse(response.StatusCode.ToString()));
      }

      return Result<string>.Success(content, int.Parse(response.StatusCode.ToString()));
    }
    catch (Exception ex)
    {
      return Result<string>.Failure(new List<Error>
      {
        new Error(ex.Message, ex.InnerException!.ToString())
      }, StatusCodes.Status502BadGateway);
    }
  }

  public Task<Result<string>> CreateShippingOrderAsync(object shippingData) =>
      SendRequestAsync(CreateRequest(HttpMethod.Post, "v2/shipping-order/create", shippingData));

  public Task<Result<string>> GetShippingFeeAsync(object feeData) =>
      SendRequestAsync(CreateRequest(HttpMethod.Post, "v2/shipping-order/fee", feeData));

  public Task<Result<string>> GetOrderTrackingAsync(string orderCode) =>
      SendRequestAsync(CreateRequest(HttpMethod.Get, $"v2/shipping-order/detail?order_code={orderCode}"));

  public Task<Result<string>> CreateReturnOrderAsync(object returnData) =>
      SendRequestAsync(CreateRequest(HttpMethod.Post, "v2/shipping-order/create", returnData));
}
