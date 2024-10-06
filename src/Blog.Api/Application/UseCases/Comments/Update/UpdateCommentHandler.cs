using System.Net;
using Blog.Api.Application.Interfaces.Comments;
using Blog.Api.Application.Response;
using Blog.Api.Domain.Interfaces;
using Blog.Api.Domain.Interfaces.Repositories;

namespace Blog.Api.Application.UseCases.Comments.Update;

public class UpdateCommentHandler(ICommentRepository commentRepository, IUnitOfWork unitOfWork) : IUpdateCommentHandler
{
    public async Task<IResponse<string>> Handle(UpdateCommentRequest request, CancellationToken cancellationToken)
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

        return await UpdateComment(request, cancellationToken);
    }
    
    private async Task<IResponse<string>> UpdateComment(UpdateCommentRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var comment = await commentRepository.GetOneAsync(request.CommentId, request.UserId);
            if (comment == null)
                return new Response<string>(HttpStatusCode.NotFound, null, "Comentário não encontrado.");

            comment.UpdateDescription(request.Description);

            await commentRepository.UpdateAsync(comment);
            await unitOfWork.CommitAsync(cancellationToken);

            return new Response<string>(HttpStatusCode.NoContent, null, "Comentário atualizado com sucesso.");
        }
        catch (Exception e)
        {
            unitOfWork.Rollback();
            throw new Exception($"Falha ao atualizar comentário. Detalhes: {e.Message}");
        }
        finally
        {
            unitOfWork.Dispose();
        }
    }
}