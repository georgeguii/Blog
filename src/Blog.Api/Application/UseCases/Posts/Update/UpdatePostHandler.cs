using Blog.Api.Application.Interfaces.Posts;
using Blog.Api.Domain.Interfaces;

namespace Blog.Api.Application.UseCases.Posts.Update;

public class UpdatePostHandler : IUpdatePostHandler
{
    public Task<IResponse> Handle(UpdatePostRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}