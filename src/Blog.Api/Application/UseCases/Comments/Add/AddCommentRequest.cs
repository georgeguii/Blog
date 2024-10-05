using Blog.Api.Application.Validators.Comments;
using FluentValidation.Results;

namespace Blog.Api.Application.UseCases.Comments.Add;

public class AddCommentRequest
{
    public Guid UserId { get; set; }
    public string Description { get; set; }
    
    public ValidationResult Validate()
    {
        var validator = new AddCommentValidator();
        return validator.Validate(this);
    }
}