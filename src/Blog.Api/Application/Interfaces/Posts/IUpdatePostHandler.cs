using Blog.Api.Application.UseCases.Posts.Update;
using Blog.Api.Domain.Interfaces;

namespace Blog.Api.Application.Interfaces.Posts;

public interface IUpdatePostHandler
{
    Task<IResponse> Handle(UpdatePostRequest request, CancellationToken cancellationToken);
}