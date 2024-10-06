using Blog.Api.Application.UseCases.LikePosts.Remove;
using Blog.Api.Domain.Interfaces;

namespace Blog.Api.Application.Interfaces.LikesPosts;

public interface IRemoveLikePostHandler
{
    Task<IResponse<string>> Handle(RemoveLikePostRequest request, CancellationToken cancellationToken);
}