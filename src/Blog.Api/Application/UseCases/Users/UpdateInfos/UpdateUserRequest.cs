using Blog.Api.Application.Validators.Users;
using FluentValidation.Results;

namespace Blog.Api.Application.UseCases.Users.UpdateInfos;

public class UpdateUserRequest
{
    public Guid UserId { get; set; }
    public string Nickname { get; set;}
    public string Name { get; set;}
    
    public ValidationResult Validate()
    {
        var validator = new UpdateUserValidator();
        return validator.Validate(this);
    }
}