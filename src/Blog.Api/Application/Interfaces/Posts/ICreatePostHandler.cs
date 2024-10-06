using Blog.Api.Application.UseCases.Posts.Create;
using Blog.Api.Domain.Interfaces;

namespace Blog.Api.Application.Interfaces.Posts;

public interface ICreatePostHandler
{
    Task<IResponse<CreatePostResponse>> Handle(CreatePostRequest request, CancellationToken cancellationToken);
}