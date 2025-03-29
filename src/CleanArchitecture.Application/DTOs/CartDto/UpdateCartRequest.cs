namespace CleanArchitecture.Application.DTOs.CartDto;

public record UpdateCartRequest(Guid CartId, List<UpdateCartItemDto> Items);

public record UpdateCartItemDto(Guid CosmeticId, int Quantity);
