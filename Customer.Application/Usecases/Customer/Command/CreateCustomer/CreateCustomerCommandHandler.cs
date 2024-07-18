using System.Net;
using Customer.Application.Interfaces;
using Customer.Domain.Entities;
using Customer.Domain.Repositories;
using Customer.Shared.Commons;
using MediatR;
using Shared.Messages;

namespace Customer.Application.Usecases.Customer.Command.CreateCustomer;

public class CreateCustomerCommandHandler(ICustomerRepository _customerRepository, IMessageHandlerService _msg): IRequestHandler<CreateCustomerCommand, CreateCustomerResult>
{
    
    public async Task<CreateCustomerResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var requestCustomeRepository = new CustomerEntity()
            {
                Id = Guid.NewGuid().ToString(),
                Document = request.Document,
                Email = request.Email,
                Name = request.Name,
                Phone = request.Phone,
                ProfileTypeId = request.ProfileType
            };

            var response = await _customerRepository.Insert(requestCustomeRepository, cancellationToken);
            if (string.IsNullOrEmpty(response.Document))
            {
                _msg
                    .AddError()
                    .WithStatusCode(HttpStatusCode.BadRequest)
                    .WithMessage(MessagesConsts.ErrorDefault)
                    .WithErrorCode(Guid.NewGuid().ToString())
                    .Commit();
            
                return new(); 
            }

            return new ()
            {
                Id = response.Id,
                Document = response.Document,
                Email = response.Email,
                Name = response.Name,
                Phone = response.Phone,
                ProfileType = response.ProfileTypeId
            };
        
        }
        catch (Exception e)
        {
            _msg
                .AddError()
                .WithMessage(MessagesConsts.ErrorDefault)
                .WithStatusCode(HttpStatusCode.UnprocessableEntity)
                .WithErrorCode(Guid.NewGuid().ToString())
                .WithStackTrace(e.StackTrace)
                .Commit();

            return new();
        }
    }
}