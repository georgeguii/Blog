using Blog.Api.Application.Validators.Post;
using FluentValidation.Results;

namespace Blog.Api.Application.UseCases.Posts.Update;

public class UpdatePostRequest
{
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
    public string Description { get; set; }
    
    public ValidationResult Validate()
    {
        var validator = new UpdatePostValidator();
        return validator.Validate(this);
    }
}