using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Shared.Messages;

public static class CustomErrorResponses
{
    public static IActionResult InternalServerError(object result)
    {
        return new ObjectResult(result)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError
        };
    }
}