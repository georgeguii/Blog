﻿using Blog.Domain.Interfaces;

namespace Blog.Application.Interfaces.Comment;

public interface ICreateCommentHandler
{
    Task<IResponse> Handle( request, CancellationToken cancellationToken);
}
