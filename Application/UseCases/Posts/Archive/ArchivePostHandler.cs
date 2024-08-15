using Blog.Application.Interfaces.Posts;
using Blog.Domain.Entities;
using Blog.Domain.Interfaces;
using Blog.Domain.Interfaces.Repositories;
using System.Net;

namespace Blog.Application.UseCases.Posts.Archive;

public class ArchivePostHandler : IArchivePostHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPostRepository _postRepository;

    public ArchivePostHandler(IUnitOfWork unitOfWork, IPostRepository postRepository)
    {
        _unitOfWork = unitOfWork;
        _postRepository = postRepository;
    }

    public async Task<IResponse> Handle(ArchivePostRequest request, CancellationToken cancellationToken)
    {
        var response = new Response<Post>();

        var post = await _postRepository.GetOneAsync(request.PostId);

        if (post == null)
        {
            response.StatusCode = HttpStatusCode.NotFound;
            response.Message = "Post not found";
            return response;
        }

        post.Archive();

        await _postRepository.UpdateAsync(post);

        await _unitOfWork.CommitAsync(cancellationToken);

        response.StatusCode = HttpStatusCode.NoContent;
        response.Message = "Post archived successfully";

        return response;
    }
}
