using Blog.Api.Application.Validators.Post;
using FluentValidation.Results;

namespace Blog.Api.Application.UseCases.Posts.Delete;

public class DeletePostRequest
{
    public Guid PostId { get;}
    public Guid UserId { get; }

    public DeletePostRequest(Guid postId, Guid userId)
    {
        PostId = postId;
        UserId = userId;
    }

    public ValidationResult Validate()
    {
        var validator = new DeletePostValidator();
        return validator.Validate(this);
    }
}