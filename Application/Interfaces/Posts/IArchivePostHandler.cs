using Blog.Application.UseCases.Posts.Archive;
using Blog.Domain.Interfaces;

namespace Blog.Application.Interfaces.Posts;

public interface IArchivePostHandler
{
    Task<IResponse> Handle(ArchivePostRequest request, CancellationToken cancellationToken);
}
