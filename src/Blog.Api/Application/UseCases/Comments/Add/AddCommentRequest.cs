using System.Text.Json.Serialization;
using Blog.Api.Application.Validators.Comments;
using FluentValidation.Results;

namespace Blog.Api.Application.UseCases.Comments.Add;

public class AddCommentRequest
{
    [JsonIgnore]
    public Guid UserId { get; set; }
    [JsonIgnore]
    public Guid PostId { get; set; }
    public string Description { get; set; }
    
    public ValidationResult Validate()
    {
        var validator = new AddCommentValidator();
        return validator.Validate(this);
    }
}