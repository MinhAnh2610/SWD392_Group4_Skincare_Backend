namespace CleanArchitecture.Domain.Entities;

public class Batch : Entity<Guid>
{
  public Guid CosmeticId { get; set; }
  public Cosmetic Cosmetic { get; set; } = default!;
  public int Quantity { get; set; }
  public DateOnly ExportedDate { get; set; }
  public DateOnly ManufactureDate { get; set; }
  public DateOnly ExpirationDate { get; set; }
}
