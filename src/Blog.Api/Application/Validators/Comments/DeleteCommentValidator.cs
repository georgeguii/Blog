using Blog.Api.Application.UseCases.Comments.Delete;
using FluentValidation;

namespace Blog.Api.Application.Validators.Comments;

public class DeleteCommentValidator : AbstractValidator<DeleteCommentRequest>
{
    public DeleteCommentValidator()
    {
        RuleFor(p => p.CommentId)
            .NotNull().WithMessage("O PostId é obrigatório.")
            .NotEmpty().WithMessage("O PostId não pode ser vazio.");
            
        RuleFor(p => p.UserId)
            .NotNull().WithMessage("O UserId é obrigatório.")
            .NotEmpty().WithMessage("O UserId não pode ser vazio.");
    }
}