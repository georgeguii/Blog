using System.Net;
using Blog.Api.Application.Interfaces.LikesPosts;
using Blog.Api.Application.Response;
using Blog.Api.Domain.Entities;
using Blog.Api.Domain.Interfaces;
using Blog.Api.Domain.Interfaces.Repositories;

namespace Blog.Api.Application.UseCases.LikePosts.Give;

public class GiveLikePostHandler(IUnitOfWork unitOfWork, ILikeRepository repository) : IGiveLikePostHandler
{
    public async Task<IResponse<string>> Handle(GiveLikePostRequest request, CancellationToken cancellationToken)
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

        return await GiveLike(request, cancellationToken);
    }

    private async Task<IResponse<string>> GiveLike(GiveLikePostRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var like = await repository.GetOneAsync(request.UserId, request.PostId);
            
            if (like != null)
            {
                return new Response<string>(
                    HttpStatusCode.Conflict,
                    null,
                    "Você já curtiu este post.");
            }

            var newLike = new Like(request.UserId, request.PostId);

            unitOfWork.BeginTransaction();
            await repository.CreateAsync(newLike);
            await unitOfWork.CommitAsync(cancellationToken);

            return new Response<string>(
                HttpStatusCode.Created,
                null,
                "Post curtido com sucesso.");
        }
        catch (Exception e)
        {
            unitOfWork.Rollback();
            throw new Exception($"Falha ao curtir o post. Detalhes: {e.Message}");
        }
        finally
        {
            unitOfWork.Dispose();
        }
    }
}