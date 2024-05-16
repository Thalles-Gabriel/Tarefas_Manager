using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace TarefasManager.Controllers;

[ApiController]
[Route("[controller]")]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(
                statusCode: statusCode,
                title: error.Code, 
                type: error.Type.ToString(), 
                detail: error.Description
                );
    }
}
