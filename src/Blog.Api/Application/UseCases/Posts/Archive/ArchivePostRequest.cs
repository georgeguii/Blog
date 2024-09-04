using Blog.Api.Application.Validators.Post;
using FluentValidation.Results;

namespace Blog.Api.Application.UseCases.Posts.Archive;

public class ArchivePostRequest
{
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
    
    public ValidationResult Validate()
    {
        var validator = new ArchivePostValidator();
        return validator.Validate(this);
    }
}