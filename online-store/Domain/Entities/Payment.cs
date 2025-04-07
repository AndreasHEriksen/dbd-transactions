using online_store.Domain.Entities.BE;

namespace online_store.Domain.Entities;

public class Payment
{
    private readonly PaymentBE _be;

    public Payment(Guid orderId, decimal amount, string method)
    {
        _be = new PaymentBE
        {
            Id = Guid.NewGuid(),
            OrderId = orderId,
            Amount = amount,
            Method = method,
            Status = "Pending",
            CreatedAt = DateTime.UtcNow
        };
    }

    // For loading from persistence
    public Payment(PaymentBE be)
    {
        _be = be;
    }

    // Properties that expose BE data
    public Guid Id => _be.Id;
    public Guid OrderId => _be.OrderId;
    public decimal Amount => _be.Amount;
    public string TransactionId => _be.TransactionId;
    public string Method => _be.Method;
    public string Status => _be.Status;
    public DateTime CreatedAt => _be.CreatedAt;
    public DateTime? CompletedAt => _be.CompletedAt;

    // Get underlying backend entity for persistence
    public PaymentBE GetBE() => _be;

    // Domain methods
    public void CompletePayment(string transactionId)
    {
        _be.Status = "Completed";
        _be.TransactionId = transactionId;
        _be.CompletedAt = DateTime.UtcNow;
    }

    public void FailPayment(string reason)
    {
        _be.Status = "Failed";
        _be.TransactionId = reason;
    }
}
