using System.Net;
using Blog.Api.Application.Interfaces.Comments;
using Blog.Api.Application.Response;
using Blog.Api.Domain.Interfaces;
using Blog.Api.Domain.Interfaces.Repositories;

namespace Blog.Api.Application.UseCases.Comments.Delete;

public class DeleteCommentHandler(IUnitOfWork unitOfWork, ICommentRepository repository) : IDeleteCommentHandler
{
    public async Task<IResponse> Handle(DeleteCommentRequest request, CancellationToken cancellationToken)
    {
        var requestValidated = request.Validate();

        if (!requestValidated.IsValid)
        {
            var response = new Response<string>(
                HttpStatusCode.BadRequest,
                "Requisição inválida",
                requestValidated.ToDictionary());
            return response;
        }

        return await DeleteComment(request, cancellationToken);
    }
    
    private async Task<IResponse> DeleteComment(DeleteCommentRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var comment = await repository.GetOneAsync(request.CommentId, request.UserId);
            if (comment == null)
                return new Response<string>(HttpStatusCode.NotFound, null, "Comentário não encontrado.");

            await repository.DeleteAsync(comment);
            await unitOfWork.CommitAsync(cancellationToken);

            return new Response<string>(HttpStatusCode.OK, null, "Comentário apagado com sucesso.");
        }
        catch (Exception e)
        {
            unitOfWork.Rollback();
            throw new Exception($"Falha ao deletar comentário. Detalhes: {e.Message}");
        }
        finally
        {
            unitOfWork.Dispose();
        }
    }
}