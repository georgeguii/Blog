using Blog.Application.UseCases.Posts.Update;
using Blog.Domain.Interfaces;

namespace Blog.Application.Interfaces.Posts;

public interface IUpdatePostHandler
{
    Task<IResponse> Handle(UpdatePostRequest request, CancellationToken cancellationToken);
}
