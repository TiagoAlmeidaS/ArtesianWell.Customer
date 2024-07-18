using System.Net;
using Customer.Domain.Repositories;
using Customer.Shared.Commons;
using MediatR;
using Shared.Messages;

namespace Customer.Application.Usecases.Customer.Query.GetCustomer;

public class GetCustomerQueryHandler(ICustomerRepository customerRepository, IMessageHandlerService msg): IRequestHandler<GetCustomerQuery, GetCustomerResult>
{
    public async Task<GetCustomerResult> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await customerRepository.GetWhere(c => c.Document == request.Document || c.Email == request.Email, cancellationToken);
            if (!response.Any())
            {
                msg
                    .AddError()
                    .WithStatusCode(HttpStatusCode.NotFound)
                    .WithMessage(MessagesConsts.ErrorCustomerNotfound)
                    .WithErrorCode(Guid.NewGuid().ToString())
                    .Commit();
            
                return new(); 
            }

            var customer = response.First();
            
            return new ()
            {
                Document = customer.Document,
                Email = customer.Email,
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                ProfileType = customer.ProfileTypeId
            };
        
        }
        catch (Exception e)
        {
            msg
                .AddError()
                .WithMessage(MessagesConsts.ErrorDefault)
                .WithStatusCode(HttpStatusCode.InternalServerError)
                .WithErrorCode(Guid.NewGuid().ToString())
                .WithStackTrace(e.StackTrace)
                .Commit();

            return new();
        }
    }   
}