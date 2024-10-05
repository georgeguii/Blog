using System.Net;
using Blog.Api.Application.Interfaces.Comments;
using Blog.Api.Application.Response;
using Blog.Api.Domain.Entities;
using Blog.Api.Domain.Interfaces;
using Blog.Api.Domain.Interfaces.Repositories;

namespace Blog.Api.Application.UseCases.Comments.Add;

public class AddCommentHandler(IUnitOfWork unitOfWork, ICommentRepository repository) : IAddCommentHandler
{
    public async Task<IResponse> Handle(AddCommentRequest request, CancellationToken cancellationToken)
    {
        var requestValidated = request.Validate();

        if (requestValidated.IsValid)
            return await CreatePost(request, cancellationToken);
        
        
        var response = new Response<string>(
            HttpStatusCode.BadRequest,
            "Requisição inválida",
            requestValidated.ToDictionary());
        return response;

    }

    private async Task<IResponse> CreatePost(AddCommentRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var comment = new Comment(
                request.Description
            );

            unitOfWork.BeginTransaction();
            await repository.CreateAsync(comment, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            return new Response<AddCommentResponse>(
                HttpStatusCode.Created,
                new AddCommentResponse(comment.Id),
                "Post criado com sucesso");
        }
        catch (Exception e)
        {
            unitOfWork.Rollback();
            throw new Exception($"Falha ao criar post. Detalhes: {e.Message}");
        }
        finally
        {
            unitOfWork.Dispose();
        }
    }
}