using Blog.Api.Application.Validators.Likes;
using FluentValidation.Results;

namespace Blog.Api.Application.UseCases.LikePosts.Remove;

public class RemoveLikePostRequest
{
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }

    public ValidationResult Validate()
    {
        var validator = new RemoveLikePostValidator();
        return validator.Validate(this);
    }
}