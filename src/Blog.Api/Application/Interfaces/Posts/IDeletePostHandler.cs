using Blog.Api.Application.UseCases.Posts.Delete;
using Blog.Api.Domain.Interfaces;

namespace Blog.Api.Application.Interfaces.Posts;

public interface IDeletePostHandler
{
    Task<IResponse> Handle(DeletePostRequest request, CancellationToken cancellationToken);
}