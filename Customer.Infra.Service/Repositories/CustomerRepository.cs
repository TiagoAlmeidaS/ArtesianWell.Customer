using System.Linq.Expressions;
using System.Net;
using Customer.Application.Interfaces;
using Customer.Domain.Entities;
using Customer.Domain.Repositories;
using Customer.Domain.SeedWork.GenericRepositories;
using Customer.Infra.Service.Context;
using Customer.Shared.Commons;
using Customer.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using Shared.Messages;

namespace Customer.Infra.Service.Repositories;

public class CustomerRepository(CustomerDBContext _context, IMessageHandlerService msg, IUnitOfWork unitOfWork): ICustomerRepository
{
    private DbSet<CustomerEntity> _userDb => _context.Set<CustomerEntity>();

    async Task<CustomerEntity> IInsertRepository<CustomerEntity>.Insert(CustomerEntity aggregate,
        CancellationToken cancellationToken)
    {
        
        var existingCustomer = await _userDb.FirstOrDefaultAsync(c => c.Document == aggregate.Document);
        if (existingCustomer != null)
        {
            msg.AddError()
                .WithMessage(MessagesConsts.ErrorCustomerAlreadyExists)
                .WithStatusCode(HttpStatusCode.BadRequest)
                .WithErrorCode(Guid.NewGuid().ToString())
                .Commit();
            
            return null;
        }

        if (!CustomerUtils.ValidationProfileType(aggregate.ProfileTypeId))
        {
            msg.AddError()
                .WithMessage(MessagesConsts.ErrorCustomerInvalidProfileType)
                .WithStatusCode(HttpStatusCode.BadRequest)
                .WithErrorCode(Guid.NewGuid().ToString())
                .Commit();
            
            return null;
        }
        
        var response = await _userDb.AddAsync(aggregate, cancellationToken);
        await unitOfWork.Commit(cancellationToken);
        
        return response.Entity;
    }

    public async Task<IList<CustomerEntity>> GetWhere(Expression<Func<CustomerEntity, bool>> predicate,
        CancellationToken cancellationToken)
    {
        try
        {
            return await _userDb.AsNoTracking().Where(predicate).ToListAsync(cancellationToken);
        }
        catch (Exception e)
        {
            msg.AddError()
                .WithMessage(MessagesConsts.ErrorCustomerNotfound)
                .WithStatusCode(HttpStatusCode.InternalServerError)
                .WithErrorCode(Guid.NewGuid().ToString())
                .WithStackTrace(e.StackTrace)
                .Commit();

            return new List<CustomerEntity>();
        }
    }

    async Task<CustomerEntity> IUpdateRepository<CustomerEntity>.Update(CustomerEntity aggregate, CancellationToken cancellationToken)
    {
        var entity = _userDb.Find(aggregate.Document);
        if (entity is null)
        {
            msg
                .AddError()
                .WithMessage(MessagesConsts.ErrorSaveCustomer)
                .WithStatusCode(HttpStatusCode.InternalServerError)
                .WithErrorCode(Guid.NewGuid().ToString())
                .Commit();
        }
        
        entity = aggregate;
        var response  = _userDb.Update(entity);
        return response.Entity;
    }
    
}