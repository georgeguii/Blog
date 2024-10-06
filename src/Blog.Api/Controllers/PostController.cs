using System.Net;
using Blog.Api.Application.Interfaces.Comments;
using Blog.Api.Application.Interfaces.LikesPosts;
using Blog.Api.Application.Interfaces.Posts;
using Blog.Api.Application.UseCases.Comments.Add;
using Blog.Api.Application.UseCases.Comments.Delete;
using Blog.Api.Application.UseCases.Comments.Update;
using Blog.Api.Application.UseCases.LikePosts.Give;
using Blog.Api.Application.UseCases.LikePosts.Remove;
using Blog.Api.Application.UseCases.Posts.Archive;
using Blog.Api.Application.UseCases.Posts.Create;
using Blog.Api.Application.UseCases.Posts.Delete;
using Blog.Api.Application.UseCases.Posts.Update;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PostController(
    ICreatePostHandler createHandler,
    IDeletePostHandler deleteHandler,
    IUpdatePostHandler updateHandler,
    IArchivePostHandler archiveHandler,
    IGiveLikePostHandler likeHandler,
    IRemoveLikePostHandler removeLikeHandler,
    IAddCommentHandler addCommentHandler,
    IDeleteCommentHandler deleteCommentHandler,
    IUpdateCommentHandler updateCommentHandler) : ControllerBase
{
[HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostRequest request,
        CancellationToken cancellationToken)
    {
        request.UserId = Guid.NewGuid();
        var response = await createHandler.Handle(request, cancellationToken);

        return response.StatusCode switch
        {
            HttpStatusCode.Created => Ok(response),
            HttpStatusCode.BadRequest => BadRequest(response),
            HttpStatusCode.Conflict => Conflict(response),
            _ => StatusCode((int)response.StatusCode, response)
        };
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(Guid id, CancellationToken cancellationToken)
    {
        var userId = Guid.NewGuid();
        var request = new DeletePostRequest(id, userId);
        
        var response = await deleteHandler.Handle(request, cancellationToken);

        return response.StatusCode switch
        {
            HttpStatusCode.NoContent => NoContent(),
            HttpStatusCode.NotFound => NotFound(response),
            _ => StatusCode((int)response.StatusCode, response)
        };
    }

    [HttpPut("{postId}")]
    public async Task<IActionResult> UpdatePost(Guid postId, [FromBody] UpdatePostRequest request,
        CancellationToken cancellationToken)
    {
        request.PostId = postId;
        request.UserId = Guid.NewGuid();
        
        var response = await updateHandler.Handle(request, cancellationToken);

        return response.StatusCode switch
        {
            HttpStatusCode.NoContent => NoContent(),
            HttpStatusCode.BadRequest => BadRequest(response),
            HttpStatusCode.NotFound => NotFound(response),
            HttpStatusCode.Conflict => Conflict(response),
            _ => StatusCode((int)response.StatusCode, response)
        };
    }

    [HttpPut("{postId}/archive")]
    public async Task<IActionResult> ArchivePost(Guid postId, CancellationToken cancellationToken)
    {
        var userId = Guid.NewGuid();
        var request = new ArchivePostRequest(userId, postId);
        
        var response = await archiveHandler.Handle(request, cancellationToken);

        return response.StatusCode switch
        {
            HttpStatusCode.NoContent => NoContent(),
            HttpStatusCode.NotFound => NotFound(response),
            _ => StatusCode((int)response.StatusCode, response)
        };
    }

    [HttpPost("{postId}/like")]
    public async Task<IActionResult> GiveLike(Guid postId, CancellationToken cancellationToken)
    {
        var userId = Guid.NewGuid();
        var request = new GiveLikePostRequest(userId, postId);
        
        var response = await likeHandler.Handle(request, cancellationToken);

        return response.StatusCode switch
        {
            HttpStatusCode.Created => Ok(response),
            HttpStatusCode.BadRequest => BadRequest(response),
            HttpStatusCode.Conflict => Conflict(response),
            _ => StatusCode((int)response.StatusCode, response)
        };
    }

    [HttpDelete("{postId}/like")]
    public async Task<IActionResult> RemoveLike(Guid postId, CancellationToken cancellationToken)
    {
        var userId = Guid.NewGuid();
        var request = new RemoveLikePostRequest(userId, postId);
        
        var response = await removeLikeHandler.Handle(request, cancellationToken);

        return response.StatusCode switch
        {
            HttpStatusCode.NoContent => NoContent(),
            HttpStatusCode.NotFound => NotFound(response),
            _ => StatusCode((int)response.StatusCode, response)
        };
    }
    
    [HttpPost("{postId}/comments")]
    public async Task<IActionResult> AddComment(Guid postId, [FromBody] AddCommentRequest request,
        CancellationToken cancellationToken)
    {
        request.PostId = postId;
        request.UserId = Guid.NewGuid();

        var response = await addCommentHandler.Handle(request, cancellationToken);

        return response.StatusCode switch
        {
            // Teste para deixar generico
            HttpStatusCode.NoContent => Ok(),
            _ => StatusCode((int)response.StatusCode, response)
        };
    }
    
    // TODO: Verificar necessidade de utilização do postId
    [HttpPut("{postId}/comments/{commentId}")]
    public async Task<IActionResult> UpdateComment(Guid commentId, [FromBody] UpdateCommentRequest request,
        CancellationToken cancellationToken)
    {
        request.CommentId = commentId;
        request.UserId = Guid.NewGuid();

        var response = await updateCommentHandler.Handle(request, cancellationToken);

        return response.StatusCode switch
        {
            HttpStatusCode.NoContent => NoContent(),
            HttpStatusCode.BadRequest => BadRequest(response),
            HttpStatusCode.NotFound => NotFound(response),
            HttpStatusCode.Conflict => Conflict(response),
            _ => StatusCode((int)response.StatusCode, response)
        };
    }

    // TODO: Verificar necessidade de utilização do postId
    [HttpDelete("{postId}/comments/{commentId}")]
    public async Task<IActionResult> DeleteComment(Guid commentId, CancellationToken cancellationToken)
    {
        var userId = Guid.NewGuid();
        var request = new DeleteCommentRequest(userId, commentId);

        var response = await deleteCommentHandler.Handle(request, cancellationToken);

        return response.StatusCode switch
        {
            HttpStatusCode.NoContent => NoContent(),
            HttpStatusCode.NotFound => NotFound(response),
            _ => StatusCode((int)response.StatusCode, response)
        };
    }
}