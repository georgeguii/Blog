using System.Net;
using Blog.Api.Application.Interfaces.Users;
using Blog.Api.Application.UseCases.Users.Create;
using Blog.Api.Application.UseCases.Users.DisableAccount;
using Blog.Api.Application.UseCases.Users.UpdateInfos;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController(
    ICreateUserHandler createHandler,
    IDisableUserHandler disableHandler,
    IUpdateInfosUserHandler updateHandler) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request,
        CancellationToken cancellationToken)
    {
        var response = await createHandler.Handle(request, cancellationToken);

        return response.StatusCode switch
        {
            HttpStatusCode.Created => Ok(response),
            HttpStatusCode.BadRequest => BadRequest(response),
            HttpStatusCode.Conflict => Conflict(response),
            _ => StatusCode((int)response.StatusCode, response)
        };
    }

    [HttpPut]
    [Route("disable")]
    public async Task<IActionResult> DisableUser([FromBody] DisableUserRequest request,
        CancellationToken cancellationToken)
    {
        var response = await disableHandler.Handle(request, cancellationToken);

        return response.StatusCode switch
        {
            HttpStatusCode.NoContent => NoContent(),
            HttpStatusCode.NotFound => NotFound(response),
            _ => StatusCode((int)response.StatusCode, response)
        };
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUserInfos([FromBody] UpdateUserRequest request,
        CancellationToken cancellationToken)
    {
        var response = await updateHandler.Handle(request, cancellationToken);

        return response.StatusCode switch
        {
            HttpStatusCode.NoContent => NoContent(),
            HttpStatusCode.BadRequest => BadRequest(response),
            HttpStatusCode.NotFound => NotFound(response),
            HttpStatusCode.Conflict => Conflict(response),
            _ => StatusCode((int)response.StatusCode, response)
        };
    }
}