using Blog.Api.Application.Interfaces.Posts;
using Blog.Api.Domain.Interfaces;

namespace Blog.Api.Application.UseCases.Posts.Create;

public class CreatePostHandler : ICreatePostHandler
{
    public Task<IResponse> Handle(CreatePostRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}