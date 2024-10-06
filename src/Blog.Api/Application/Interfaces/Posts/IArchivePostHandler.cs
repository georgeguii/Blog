using Blog.Api.Application.UseCases.Posts.Archive;
using Blog.Api.Domain.Interfaces;

namespace Blog.Api.Application.Interfaces.Posts;

public interface IArchivePostHandler
{
    Task<IResponse<string>> Handle(ArchivePostRequest request, CancellationToken cancellationToken);
}