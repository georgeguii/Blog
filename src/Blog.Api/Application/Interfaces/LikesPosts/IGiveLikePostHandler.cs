using Blog.Api.Application.UseCases.LikePosts.Give;
using Blog.Api.Domain.Interfaces;

namespace Blog.Api.Application.Interfaces.LikesPosts;

public interface IGiveLikePostHandler
{
    Task<IResponse> Handle(GiveLikePostRequest request, CancellationToken cancellationToken);
}