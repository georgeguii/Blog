﻿using Blog.Application.Interfaces.Post;
using Blog.Domain.Interfaces;

namespace Blog.Application.UseCases.Posts.Update;

public class UpdatePostHandler : IUpdatePostHandler
{
    public Task<IResponse> Handle(UpdatePostRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
