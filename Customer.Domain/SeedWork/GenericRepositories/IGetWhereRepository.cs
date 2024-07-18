using System.Linq.Expressions;

namespace Customer.Domain.SeedWork.GenericRepositories;

public interface IGetWhereRepository<TAggregate> : IRepository
    where TAggregate : AggregateRoot
{
    public Task<IList<TAggregate>> GetWhere(Expression<Func<TAggregate, bool>> predicate, CancellationToken cancellationToken);
}