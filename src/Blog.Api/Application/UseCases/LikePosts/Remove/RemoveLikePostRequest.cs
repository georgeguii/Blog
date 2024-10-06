using System.Text.Json.Serialization;
using Blog.Api.Application.Validators.Likes;
using FluentValidation.Results;

namespace Blog.Api.Application.UseCases.LikePosts.Remove;

public class RemoveLikePostRequest
{
    [JsonIgnore]
    public Guid UserId { get; }
    public Guid PostId { get; }

    public RemoveLikePostRequest(Guid userId, Guid postId)
    {
        UserId = userId;
        PostId = postId;
    }

    public ValidationResult Validate()
    {
        var validator = new RemoveLikePostValidator();
        return validator.Validate(this);
    }
}