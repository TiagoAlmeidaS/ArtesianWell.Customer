using System.Net;
using Customer.Shared.Messages;
using Microsoft.AspNetCore.Mvc;
using Shared.Messages;

namespace Customer.Shared.Utils;

[Route("api/[controller]")]
[Produces("application/json")]
[ApiController]
public class ArtesianWellBaseController: ControllerBase
{
    private readonly IMessageHandlerService _errorWarningHandlingService;

    public ArtesianWellBaseController(IMessageHandlerService errorWarningHandlingService)
    {
        _errorWarningHandlingService = errorWarningHandlingService;
    }

    public IActionResult HandleResult<T>(T result)
    {
        var formatedResult = result.MakeResponse(_errorWarningHandlingService);

        if (_errorWarningHandlingService.HasErrors)
            return GetErrorResult(formatedResult);
        
        return Ok(formatedResult);
    }

    public IActionResult GetErrorResult(object formatedResult)
    {
        var errorWithCustomErrorCode = _errorWarningHandlingService.GetErrors().Where(x => x.CustomStatusCode);

        if (!errorWithCustomErrorCode.Any())
            return Ok(formatedResult);
        
        return errorWithCustomErrorCode.First().StatusCode switch
        {
            HttpStatusCode.NotFound => NotFound(formatedResult),
            HttpStatusCode.Unauthorized => Unauthorized(formatedResult),
            HttpStatusCode.BadRequest => BadRequest(formatedResult),
            HttpStatusCode.UnprocessableEntity => UnprocessableEntity(formatedResult),
            HttpStatusCode.InternalServerError => CustomErrorResponses.InternalServerError(formatedResult),
            _ => BadRequest(formatedResult)
        };
    }
}