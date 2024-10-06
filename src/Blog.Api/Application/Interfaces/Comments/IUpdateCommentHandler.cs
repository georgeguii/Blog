using Blog.Api.Application.UseCases.Comments.Update;
using Blog.Api.Domain.Interfaces;

namespace Blog.Api.Application.Interfaces.Comments;

public interface IUpdateCommentHandler
{
    Task<IResponse<string>> Handle(UpdateCommentRequest request, CancellationToken cancellationToken);
}