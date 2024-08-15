using Blog.Application.Interfaces.Posts;
using Blog.Domain.Entities;
using Blog.Domain.Interfaces;
using Blog.Domain.Interfaces.Repositories;
using System.Net;
using Blog.Application.Response;

namespace Blog.Application.UseCases.Posts.Archive;

public class ArchivePostHandler(IUnitOfWork unitOfWork, IPostRepository postRepository) : IArchivePostHandler
{
    public async Task<IResponse> Handle(ArchivePostRequest request, CancellationToken cancellationToken)
    {
        var post = await postRepository.GetOneAsync(request.PostId);
        if (post == null)
        {
            return new Response<Post>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "Post not found"
            };
        }

        post.Archive();

        await postRepository.UpdateAsync(post);
        await unitOfWork.CommitAsync(cancellationToken);

        return new Response<Post>
        {
            StatusCode = HttpStatusCode.NoContent,
            Message = "Post archived successfully"
        };
    }
}
