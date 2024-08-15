using Blog.Application.UseCases.Posts.Delete;
using Blog.Domain.Interfaces;

namespace Blog.Application.Interfaces.Posts;

public interface IDeletePostHandler
{
    Task<IResponse> Handle(DeletePostRequest request, CancellationToken cancellationToken);
}
