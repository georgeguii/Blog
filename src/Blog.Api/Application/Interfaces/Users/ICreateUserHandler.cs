using Blog.Api.Application.UseCases.Users.Create;
using Blog.Api.Domain.Interfaces;

namespace Blog.Api.Application.Interfaces.Users;

public interface ICreateUserHandler
{
    Task<IResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken);
}