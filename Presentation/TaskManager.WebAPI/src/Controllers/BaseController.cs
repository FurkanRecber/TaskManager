using Microsoft.AspNetCore.Mvc;
using TaskManager.Common.Models.Response;

namespace TaskManager.WebAPI.Controllers.Base;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected static IActionResult ApiResult<T>(ServiceResponse<T> response)
        => new ObjectResult(response) { StatusCode = response.StatusCode };

    protected static IActionResult ApiResult(ServiceResponse response)
        => new ObjectResult(response) { StatusCode = response.StatusCode };
}