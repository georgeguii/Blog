using System.Net;
using Blog.Api.Application.Interfaces.Posts;
using Blog.Api.Application.Response;
using Blog.Api.Domain.Interfaces;
using Blog.Api.Domain.Interfaces.Repositories;

namespace Blog.Api.Application.UseCases.Posts.Update;

public class UpdatePostHandler(IPostRepository postRepository, IUnitOfWork unitOfWork) : IUpdatePostHandler
{
    public async Task<IResponse> Handle(UpdatePostRequest request, CancellationToken cancellationToken)
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

        return await UpdatePost(request, cancellationToken);
    }
    
    private async Task<IResponse> UpdatePost(UpdatePostRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var post = await postRepository.GetOneAsync(request.PostId, request.UserId);
            if (post == null)
                return new Response<string>(HttpStatusCode.NotFound, null, "Post não encontrado.");

            post.UpdateDescription(request.Description);

            await postRepository.UpdateAsync(post);
            await unitOfWork.CommitAsync(cancellationToken);

            return new Response<string>(HttpStatusCode.NoContent, null, "Post atualizado com sucesso.");
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