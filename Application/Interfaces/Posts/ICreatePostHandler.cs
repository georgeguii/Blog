using Blog.Application.UseCases.Posts.Create;
using Blog.Domain.Interfaces;

namespace Blog.Application.Interfaces.Posts;

public interface ICreatePostHandler
{
    Task<IResponse> Handle(CreatePostRequest request, CancellationToken cancellationToken);
}
