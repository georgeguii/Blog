using Blog.Api.Application.Validators.Users;
using FluentValidation.Results;

namespace Blog.Api.Application.UseCases.Users.DisableAccount;

public class DisableUserRequest
{
    public Guid UserId { get; set; }
    
    public ValidationResult Validate()
    {
        var validator = new DisableUserValidator();
        return validator.Validate(this);
    }
}