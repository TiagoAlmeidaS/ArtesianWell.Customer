using Customer.Domain.Entities;
using Customer.Domain.SeedWork.GenericRepositories;

namespace Customer.Domain.Repositories;

public interface ICustomerRepository: IInsertRepository<CustomerEntity>, IGetWhereRepository<CustomerEntity>, IUpdateRepository<CustomerEntity>
{
}