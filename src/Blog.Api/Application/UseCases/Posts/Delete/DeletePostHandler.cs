using Blog.Api.Application.Interfaces.Posts;
using Blog.Api.Domain.Interfaces;

namespace Blog.Api.Application.UseCases.Posts.Delete;

public class DeletePostHandler : IDeletePostHandler
{
    public Task<IResponse> Handle(DeletePostRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}