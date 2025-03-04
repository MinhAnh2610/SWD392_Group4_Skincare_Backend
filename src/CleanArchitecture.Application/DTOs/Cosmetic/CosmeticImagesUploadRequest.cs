using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.DTOs.Cosmetic;

public record CosmeticImagesUploadRequest(Guid CosmeticId, List<IFormFile> Images);
