using System.Net;
using Blog.Api.Application.Interfaces.Posts;
using Blog.Api.Application.Response;
using Blog.Api.Domain.Interfaces;
using Blog.Api.Domain.Interfaces.Repositories;

namespace Blog.Api.Application.UseCases.Posts.Archive;

public class ArchivePostHandler(IUnitOfWork unitOfWork, IPostRepository repository) : IArchivePostHandler
{
    public async Task<IResponse> Handle(ArchivePostRequest request, CancellationToken cancellationToken)
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

        return await ArchivePost(request, cancellationToken);
    }
    
    private async Task<IResponse> ArchivePost(ArchivePostRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var post = await repository.GetOneAsync(request.PostId, request.UserId);
            if (post == null)
                return new Response<string>(HttpStatusCode.NotFound, null, "Post não encontrado.");

            post.Archive();

            await repository.UpdateAsync(post);
            await unitOfWork.CommitAsync(cancellationToken);

            return new Response<string>(HttpStatusCode.NoContent, null, "Post arquivado com sucesso.");
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