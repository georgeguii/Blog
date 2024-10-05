﻿using Blog.Api.Application.UseCases.Comments.Add;
using FluentValidation;

namespace Blog.Api.Application.Validators.Comments;

public class AddCommentValidator : AbstractValidator<AddCommentRequest>
{
    public AddCommentValidator()
    {
        RuleFor(p => p.UserId)
            .NotNull().WithMessage("O userId é obrigatório.")
            .NotEmpty().WithMessage("O userId não pode ser vazio.");
        
        RuleFor(p => p.Description)
            .NotNull().WithMessage("A descrição é obrigatória.")
            .NotEmpty().WithMessage("A descrição não pode ser vazia.")
            .MaximumLength(256).WithMessage("O tamanho máximo da descrição é 256 caracteres.");
    }
}