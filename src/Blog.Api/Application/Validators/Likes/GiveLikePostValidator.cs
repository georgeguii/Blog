using Blog.Api.Application.UseCases.LikePosts.Give;
using FluentValidation;

namespace Blog.Api.Application.Validators.Likes;

public class GiveLikePostValidator : AbstractValidator<GiveLikePostRequest>
{
    public GiveLikePostValidator()
    {
        RuleFor(p => p.PostId)
            .NotNull().WithMessage("O PostId é obrigatório.")
            .NotEmpty().WithMessage("O PostId não pode ser vazio.");

        RuleFor(p => p.UserId)
            .NotNull().WithMessage("O UserId é obrigatório.")
            .NotEmpty().WithMessage("O UserId não pode ser vazio.");
    }
}