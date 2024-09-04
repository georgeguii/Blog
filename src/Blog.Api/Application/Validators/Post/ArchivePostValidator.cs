using Blog.Api.Application.UseCases.Posts.Archive;
using FluentValidation;

namespace Blog.Api.Application.Validators.Post;

public class ArchivePostValidator : AbstractValidator<ArchivePostRequest>
{
    public ArchivePostValidator()
    {
        RuleFor(p => p.PostId)
            .NotNull().WithMessage("O PostId é obrigatório.")
            .NotEmpty().WithMessage("O PostId não pode ser vazio.");
            
        RuleFor(p => p.UserId)
            .NotNull().WithMessage("O UserId é obrigatório.")
            .NotEmpty().WithMessage("O UserId não pode ser vazio.");
    }
}