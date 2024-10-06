using Blog.Api.Application.UseCases.Users.UpdateInfos;
using FluentValidation;

namespace Blog.Api.Application.Validators.Users;

public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserValidator()
    {
        RuleFor(p => p.UserId)
            .NotNull().WithMessage("O UserId é obrigatório.")
            .NotEmpty().WithMessage("O UserId não pode ser vazio.");
        
        RuleFor(x => x.Nickname)
            .NotEmpty().WithMessage("O apelido é obrigatório.")
            .MaximumLength(30).WithMessage("O apelido não pode exceder 30 caracteres.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MaximumLength(200).WithMessage("O nome não pode exceder 200 caracteres.");
    }
}