namespace CleanArchitecture.Application.DTOs.OrderDto;

public record CreateOrderRequest(
    Guid CartId,
    Guid? CouponId,
    string ShippingAddress,
    string BillingAddress,
    string PaymentMethod,
    string Currency,
    string WardCode,
    int DistrictId
);