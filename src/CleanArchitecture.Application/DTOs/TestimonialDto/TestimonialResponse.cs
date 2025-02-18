namespace CleanArchitecture.Application.DTOs.TestimonialDto;

public class TestimonialResponse
{
  public Guid Id { get; set; }
  public Guid CustomerId { get; set; }
  public string CustomerName { get; set; } = default!;
  public string Content { get; set; } = default!;
  public int Rating { get; set; }
}
