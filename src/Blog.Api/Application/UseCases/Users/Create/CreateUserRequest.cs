using Blog.Api.Application.Validators.Users;
using FluentValidation.Results;

namespace Blog.Api.Application.UseCases.Users.Create;

public class CreateUserRequest
{
    public string Email { get; set;}
    public string Password { get; set;}
    public string Nickname { get; set;}
    public string Name { get; set;}
    public string Document { get; set;}
    
    public ValidationResult Validate()
    {
        var validator = new CreateUserValidator();
        return validator.Validate(this);
    }
}