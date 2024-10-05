using Blog.Api.Application.Validators.Comments;
using FluentValidation.Results;

namespace Blog.Api.Application.UseCases.Comments.Delete;

public class DeleteCommentRequest
{
    public Guid CommentId { get; set; }
    public Guid UserId { get; set; }

    public ValidationResult Validate()
    {
        var validator = new DeleteCommentValidator();
        return validator.Validate(this);
    }
}