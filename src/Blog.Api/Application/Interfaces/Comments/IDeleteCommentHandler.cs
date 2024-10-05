using Blog.Api.Application.UseCases.Comments.Delete;
using Blog.Api.Domain.Interfaces;

namespace Blog.Api.Application.Interfaces.Comments;

public interface IDeleteCommentHandler
{
    Task<IResponse> Handle(DeleteCommentRequest request, CancellationToken cancellationToken);
}