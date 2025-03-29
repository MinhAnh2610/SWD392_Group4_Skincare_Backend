public class Payment : Entity<Guid>
{
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = default!;
    public string TransactionId { get; set; } = default!;
    public string Method { get; set; } = default!;
    public decimal TotalAmount { get; set; }
    public DateTime Date { get; set; }
}