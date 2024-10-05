using System.Net;
using Blog.Api.Application.Interfaces.LikesPosts;
using Blog.Api.Application.Response;
using Blog.Api.Domain.Interfaces;
using Blog.Api.Domain.Interfaces.Repositories;

namespace Blog.Api.Application.UseCases.LikePosts.Remove;

public class RemoveLikePostHandler(IUnitOfWork unitOfWork, ILikeRepository repository) : IRemoveLikePostHandler
{
    public async Task<IResponse> Handle(RemoveLikePostRequest request, CancellationToken cancellationToken)
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

        return await RemoveLike(request, cancellationToken);
    }

    private async Task<IResponse> RemoveLike(RemoveLikePostRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var like = await repository.GetOneAsync(request.PostId, request.UserId);
            if (like == null)
                return new Response<string>(HttpStatusCode.NotFound, null, "Like não encontrado.");

            unitOfWork.BeginTransaction();
            await repository.DeleteAsync(like);
            await unitOfWork.CommitAsync(cancellationToken);

            return new Response<string>(
                HttpStatusCode.NoContent,
                null,
                "Like removido com sucesso.");
        }
        catch (Exception e)
        {
            unitOfWork.Rollback();
            throw new Exception($"Falha ao remover o like do post. Detalhes: {e.Message}");
        }
        finally
        {
            unitOfWork.Dispose();
        }
    }
}