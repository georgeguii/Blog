using Blog.Api.Application.UseCases.Posts.Update;
using FluentValidation;

namespace Blog.Api.Application.Validators.Post;

public class UpdatePostValidator : AbstractValidator<UpdatePostRequest>
{
    public UpdatePostValidator()
    {
        RuleFor(p => p.PostId)
            .NotNull().WithMessage("O PostId é obrigatório.")
            .NotEmpty().WithMessage("O PostId não pode ser vazio.");

        RuleFor(p => p.UserId)
            .NotNull().WithMessage("O UserId é obrigatório.")
            .NotEmpty().WithMessage("O UserId não pode ser vazio.");
            
        RuleFor(p => p.Description)
            .NotNull().WithMessage("A descrição é obrigatória.")
            .NotEmpty().WithMessage("A descrição não pode ser vazia.")
            .MaximumLength(256).WithMessage("O tamanho máximo da descrição é 256 caracteres.");
    }
}