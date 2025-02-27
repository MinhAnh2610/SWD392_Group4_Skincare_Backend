namespace CleanArchitecture.Application.DTOs.FeedbackDto;

public record FeedbackRequest(Guid CosmeticId, string? Content, int Rating);
