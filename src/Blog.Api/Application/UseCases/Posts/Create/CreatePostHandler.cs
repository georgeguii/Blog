using System.Net;
using Blog.Api.Application.Interfaces.Posts;
using Blog.Api.Application.Response;
using Blog.Api.Application.UseCases.Users.Create;
using Blog.Api.Domain.Entities;
using Blog.Api.Domain.Interfaces;
using Blog.Api.Domain.Interfaces.Repositories;

namespace Blog.Api.Application.UseCases.Posts.Create;

public class CreatePostHandler(IUnitOfWork unitOfWork, IPostRepository repository) : ICreatePostHandler
{
    public async Task<IResponse<CreatePostResponse>> Handle(CreatePostRequest request, CancellationToken cancellationToken)
    {
        var requestValidated = request.Validate();

        if (!requestValidated.IsValid)
        {
            var response = new Response<CreatePostResponse>(
                HttpStatusCode.BadRequest,
                "Requisição inválida",
                requestValidated.ToDictionary());
            return response;
        }
        
        return await CreatePost(request, cancellationToken);
    }

    private async Task<IResponse<CreatePostResponse>> CreatePost(CreatePostRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var post = new Post(
                request.Description,
                request.UserId
            );

            unitOfWork.BeginTransaction();
            await repository.CreateAsync(post, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            return new Response<CreatePostResponse>(HttpStatusCode.Created, new CreatePostResponse(post.Id), 
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