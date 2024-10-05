using Blog.Api.Application.Validators.Likes;
using FluentValidation.Results;

namespace Blog.Api.Application.UseCases.LikePosts.Give;

public class GiveLikePostRequest
{
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }

    public ValidationResult Validate()
    {
        var validator = new GiveLikePostValidator();
        return validator.Validate(this);
    }
}