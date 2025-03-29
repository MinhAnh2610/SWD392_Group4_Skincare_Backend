using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Application.DTOs.BatchDto
{
  public class BatchCreateRequest
  {
    [Required]
    public Guid CosmeticId { get; set; }
    [Range(0, int.MaxValue)]
    [Required]
    public int Quantity { get; set; }
    public DateOnly ExportedDate => DateOnly.FromDateTime(DateTime.UtcNow);
    [Required]
    public DateOnly ManufactureDate { get; set; }
    [Required]
    public DateOnly ExpirationDate { get; set; }
  }
}
