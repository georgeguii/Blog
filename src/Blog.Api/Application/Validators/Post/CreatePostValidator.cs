using Blog.Api.Application.UseCases.Posts.Create;
using FluentValidation;

namespace Blog.Api.Application.Validators.Post;

public class CreatePostValidator : AbstractValidator<CreatePostRequest>
{
    public CreatePostValidator()
    {
        RuleFor(p => p.UserId)
            .NotNull().WithMessage("A userId é obrigatório.")
            .NotEmpty().WithMessage("O userId não pode ser vazio.");
        
        RuleFor(p => p.Description)
            .NotNull().WithMessage("A descrição é obrigatória.")
            .NotEmpty().WithMessage("A descrição não pode ser vazia.")
            .MaximumLength(256).WithMessage("O tamanho máximo da descrição é 256 caracteres.");
    }
}