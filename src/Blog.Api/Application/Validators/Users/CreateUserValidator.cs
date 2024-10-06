using Blog.Api.Application.UseCases.Users.Create;
using FluentValidation;

namespace Blog.Api.Application.Validators.Users;

public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O email é obrigatório.")
            .EmailAddress().WithMessage("O email deve ser um endereço de email válido.")
            .MaximumLength(150).WithMessage("O email não pode exceder 150 caracteres.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("A senha é obrigatória.")
            .MinimumLength(6).WithMessage("A senha deve ter pelo menos 6 caracteres.")
            .MaximumLength(256).WithMessage("A senha não pode exceder 256 caracteres.")
            .Matches(@"[A-Z]").WithMessage("A senha deve conter pelo menos uma letra maiúscula.")
            .Matches(@"[a-z]").WithMessage("A senha deve conter pelo menos uma letra minúscula.")
            .Matches(@"[0-9]").WithMessage("A senha deve conter pelo menos um número.")
            .Matches(@"[\W]").WithMessage("A senha deve conter pelo menos um caractere especial.");

        RuleFor(x => x.Nickname)
            .NotEmpty().WithMessage("O apelido é obrigatório.")
            .MaximumLength(30).WithMessage("O apelido não pode exceder 30 caracteres.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MaximumLength(200).WithMessage("O nome não pode exceder 200 caracteres.");

        RuleFor(x => x.Document)
            .NotEmpty().WithMessage("O CPF é obrigatório.")
            .Length(11).WithMessage("O CPF deve ter 11 caracteres.")
            .Matches("^[0-9]*$").WithMessage("O CPF deve conter apenas números.");
    }
}