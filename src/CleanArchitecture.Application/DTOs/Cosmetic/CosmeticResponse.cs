using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CleanArchitecture.Application.DTOs.Cosmetic
{
  public class CosmeticResponse
  {
    public Guid Id { get; set; }
    public DateTime? CreateAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
    public bool IsActive { get; set; }
    public Guid BrandId { get; set; }
    public Brand? Brand { get; set; }
    public Guid SkinTypeId { get; set; }
    public SkinType? SkinType { get; set; }
    public Guid CosmeticTypeId { get; set; }
    public CosmeticType? CosmeticType { get; set; }
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
    public bool Gender { get; set; } = true;
    public string Notice { get; set; } = default!;
    public string Ingredients { get; set; } = default!;
    public string MainUsage { get; set; } = default!;
    public string Texture { get; set; } = default!;
    public string Origin { get; set; } = default!;
    public string Instructions { get; set; } = default!;
    public List<CosmeticSubCategory>? CosmeticSubcategories { get; set; }
    public List<CosmeticImage>? CosmeticImages { get; set; }
    public List<Feedback>? Feedbacks { get; set; }
  }
}
