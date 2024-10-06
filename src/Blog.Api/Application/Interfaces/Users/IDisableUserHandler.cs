﻿using Blog.Api.Application.UseCases.Users.DisableAccount;
using Blog.Api.Domain.Interfaces;

namespace Blog.Api.Application.Interfaces.Users;

public interface IDisableUserHandler
{
    Task<IResponse> Handle(DisableUserRequest request, CancellationToken cancellationToken);
}