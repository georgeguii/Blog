using System.Text.Json.Serialization;
using Blog.Api.Application.Validators.Post;
using FluentValidation.Results;

namespace Blog.Api.Application.UseCases.Posts.Archive;

public class ArchivePostRequest
{
    [JsonIgnore]
    public Guid UserId { get; }
    [JsonIgnore]
    public Guid PostId { get; }

    public ArchivePostRequest(Guid userId, Guid postId)
    {
        UserId = userId;
        PostId = postId;
    }

    public ValidationResult Validate()
    {
        var validator = new ArchivePostValidator();
        return validator.Validate(this);
    }
}