using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.Cosmetic
{
  public class CosmeticResponse
  {
      // Base Entity properties
      public Guid Id { get; init; }
      public DateTime? CreateAt { get; init; }
      public string? CreatedBy { get; init; }
      public DateTime? LastModified { get; init; }
      public string? LastModifiedBy { get; init; }
      public bool IsActive { get; init; }

      // Cosmetic-specific properties
      public Guid BrandId { get; init; }
      public Guid SkinTypeId { get; init; }
      public Guid CosmeticTypeId { get; init; }
      public string Name { get; init; } = default!;
      public decimal Price { get; init; }
      public bool Gender { get; init; }
      public string Notice { get; init; } = default!;
      public string Ingredients { get; init; } = default!;
      public string MainUsage { get; init; } = default!;
      public string Texture { get; init; } = default!;
      public string Origin { get; init; } = default!;
      public string Instructions { get; init; } = default!;

}
}
