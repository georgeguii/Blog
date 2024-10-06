using System.Text.Json.Serialization;
using Blog.Api.Application.Validators.Post;
using FluentValidation.Results;

namespace Blog.Api.Application.UseCases.Posts.Update;

public class UpdatePostRequest
{
    [JsonIgnore]
    public Guid UserId { get; set; }
    [JsonIgnore]
    public Guid PostId { get; set; }
    public string Description { get; set; }
    
    public ValidationResult Validate()
    {
        var validator = new UpdatePostValidator();
        return validator.Validate(this);
    }
}