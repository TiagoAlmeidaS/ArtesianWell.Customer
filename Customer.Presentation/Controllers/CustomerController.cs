using Customer.Application.Usecases.Customer.Command.CreateCustomer;
using Customer.Application.Usecases.Customer.Query.GetCustomer;
using Customer.Shared.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Messages;
using Swashbuckle.AspNetCore.Annotations;

namespace Customer.Presentation.Controllers;

public class CustomerController(
    IMessageHandlerService errorWarningHandlingService,
    IMediator mediator)
    : ArtesianWellBaseController(errorWarningHandlingService)
{
    [HttpPost]
    [SwaggerOperation(Summary = "Customer", Description = "Create the Customer")]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
    {
        var result = await mediator.Send(command, CancellationToken.None);
        return HandleResult(result);
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Customer", Description = "Get the Customer")]
    public async Task<IActionResult> GetCustomer([FromQuery] GetCustomerQuery query)
    {
        var result = await mediator.Send(query, CancellationToken.None);
        return HandleResult(result);
    }
}