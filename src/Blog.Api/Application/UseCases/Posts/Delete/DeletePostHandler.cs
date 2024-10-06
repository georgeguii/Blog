using System.Net;
using Blog.Api.Application.Interfaces.Posts;
using Blog.Api.Application.Response;
using Blog.Api.Domain.Interfaces;
using Blog.Api.Domain.Interfaces.Repositories;

namespace Blog.Api.Application.UseCases.Posts.Delete;

public class DeletePostHandler(IUnitOfWork unitOfWork, IPostRepository repository) : IDeletePostHandler
{
    public async Task<IResponse<string>> Handle(DeletePostRequest request, CancellationToken cancellationToken)
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

        return await DeletePost(request, cancellationToken);
    }
    
    private async Task<IResponse<string>> DeletePost(DeletePostRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var post = await repository.GetOneAsync(request.PostId, request.UserId);
            if (post == null)
                return new Response<string>(HttpStatusCode.NotFound, null, "Post não encontrado.");

            await repository.DeleteAsync(post);
            await unitOfWork.CommitAsync(cancellationToken);

            return new Response<string>(HttpStatusCode.NotFound, null, "Post apagado com sucesso.");
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