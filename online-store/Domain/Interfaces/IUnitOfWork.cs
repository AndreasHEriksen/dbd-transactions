using System.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace online_store.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IOrderRepository Orders { get; }
    IProductRepository Products { get; }
    IPaymentRepository Payments { get; }
    Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel);
    Task CommitAsync();
    Task RollbackAsync();
}