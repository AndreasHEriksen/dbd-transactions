using online_store.Domain.Entities;

namespace online_store.Domain.Interfaces;

public interface IPaymentRepository
{
    Task<Payment> GetByIdAsync(Guid id);
    Task<Payment> GetByOrderIdAsync(Guid orderId);
    Task<IEnumerable<Payment>> GetAllAsync();
    Task<Payment> AddAsync(Payment payment);
    Task UpdateAsync(Payment payment);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
