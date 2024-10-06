using System.Text.Json.Serialization;
using Blog.Api.Application.Validators.Likes;
using FluentValidation.Results;

namespace Blog.Api.Application.UseCases.LikePosts.Give;

public class GiveLikePostRequest
{
    [JsonIgnore]
    public Guid UserId { get; }
    [JsonIgnore]
    public Guid PostId { get; }

    public GiveLikePostRequest(Guid userId, Guid postId)
    {
        UserId = userId;
        PostId = postId;
    }

    public ValidationResult Validate()
    {
        var validator = new GiveLikePostValidator();
        return validator.Validate(this);
    }
}