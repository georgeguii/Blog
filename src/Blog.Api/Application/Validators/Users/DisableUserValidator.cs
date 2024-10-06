using Blog.Api.Application.UseCases.Users.DisableAccount;
using FluentValidation;

namespace Blog.Api.Application.Validators.Users;

public class DisableUserValidator : AbstractValidator<DisableUserRequest>
{
    public DisableUserValidator()
    {
        RuleFor(u => u.UserId)
            .NotNull().WithMessage("O UserId é obrigatório.")
            .NotEmpty().WithMessage("O UserId não pode ser vazio.");
    }
}