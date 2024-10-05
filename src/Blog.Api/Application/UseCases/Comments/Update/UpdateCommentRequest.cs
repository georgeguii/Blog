using Blog.Api.Application.Validators.Comments;
using FluentValidation.Results;

namespace Blog.Api.Application.UseCases.Comments.Update;

public class UpdateCommentRequest
{
    public Guid CommentId { get; set; }
    public Guid UserId { get; set; }
    public string Description { get; set; }

    public ValidationResult Validate()
    {
        var validator = new UpdateCommentValidator();
        return validator.Validate(this);
    }
}