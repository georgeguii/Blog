using Blog.Api.Application.UseCases.Users.UpdateInfos;
using Blog.Api.Domain.Interfaces;

namespace Blog.Api.Application.Interfaces.Users;

public interface IUpdateInfosUserHandler
{
    Task<IResponse<string>> Handle(UpdateUserRequest request, CancellationToken cancellationToken);
}