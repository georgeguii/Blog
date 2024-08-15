using Blog.Application.Interfaces.Posts;
using Blog.Domain.Interfaces;

namespace Blog.Application.UseCases.Posts.Create;

public class CreatePostHandler : ICreatePostHandler
{
    public Task<IResponse> Handle(CreatePostRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
