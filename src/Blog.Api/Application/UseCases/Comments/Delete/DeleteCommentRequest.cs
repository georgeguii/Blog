using System.Text.Json.Serialization;
using Blog.Api.Application.Validators.Comments;
using FluentValidation.Results;

namespace Blog.Api.Application.UseCases.Comments.Delete;

public class DeleteCommentRequest
{
    [JsonIgnore]
    public Guid UserId { get; set; }
    [JsonIgnore]
    public Guid CommentId { get; set; }

    public DeleteCommentRequest(Guid userId, Guid commentId)
    {
        UserId = userId;
        CommentId = commentId;
    }

    public ValidationResult Validate()
    {
        var validator = new DeleteCommentValidator();
        return validator.Validate(this);
    }
}