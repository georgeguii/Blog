using Blog.Application.Interfaces.Post;
using Blog.Domain.Interfaces;

namespace Blog.Application.UseCases.Posts.Delete;

public class DeletePostHandler : IDeletePostHandler
{
    public Task<IResponse> Handle(DeletePostRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
