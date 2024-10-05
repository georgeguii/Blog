using Blog.Api.Application.UseCases.Comments.Add;
using Blog.Api.Domain.Interfaces;

namespace Blog.Api.Application.Interfaces.Comments;

public interface IAddCommentHandler
{
    Task<IResponse> Handle(AddCommentRequest request, CancellationToken cancellationToken);
}