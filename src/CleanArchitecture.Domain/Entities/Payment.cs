

using CleanArchitecture.Domain.Abstraction;

namespace CleanArchitecture.Domain.Entities;

public class Payment : Entity<Guid>
{
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = default!;
    public Guid RefundId { get; set; }
    public Refund Refund { get; set; } = default!;
    public string TransactionId { get; set; } = default!;
    public string Method { get; set; } = default!;
    public decimal TotalAmount { get; set; }
    public DateTime Date { get; set; }
}
