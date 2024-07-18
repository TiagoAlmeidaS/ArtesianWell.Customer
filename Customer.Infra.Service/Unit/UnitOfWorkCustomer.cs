using Customer.Application.Interfaces;
using Customer.Infra.Service.Context;

namespace Customer.Infra.Service.Unit;

public class UnitOfWorkCustomer(CustomerDBContext _context): IUnitOfWork
{
    public Task Commit(CancellationToken cancellationToken) => _context.SaveChangesAsync(cancellationToken);

    public Task Rollback(CancellationToken cancellationToken) =>
        _context.Database.RollbackTransactionAsync(cancellationToken);
}