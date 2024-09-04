using Blog.Api.Application.Validators.Post;
using FluentValidation.Results;

namespace Blog.Api.Application.UseCases.Posts.Delete;

public class DeletePostRequest
{
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
    
    public ValidationResult Validate()
    {
        var validator = new DeletePostValidator();
        return validator.Validate(this);
    }
}