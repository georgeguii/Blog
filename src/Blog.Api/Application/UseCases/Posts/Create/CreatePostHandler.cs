using System.Net;
using Blog.Api.Application.Interfaces.Posts;
using Blog.Api.Application.Response;
using Blog.Api.Domain.Entities;
using Blog.Api.Domain.Interfaces;
using Blog.Api.Domain.Interfaces.Repositories;

namespace Blog.Api.Application.UseCases.Posts.Create;

public class CreatePostHandler : ICreatePostHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPostRepository _repository;

    public CreatePostHandler(IUnitOfWork unitOfWork, IPostRepository repository)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task<IResponse> Handle(CreatePostRequest request, CancellationToken cancellationToken)
    {
        var requestValidated = request.Validate();

        if (!requestValidated.IsValid)
        {
            var response = new Response<string>(
                HttpStatusCode.BadRequest,
                "Requisição inválida",
                requestValidated.Errors
                    .GroupBy(error => error.PropertyName)
                    .ToDictionary(group => group.Key, group => group.First().ErrorMessage));
            return response;
        }


        return await CreatePost(request, cancellationToken);
    }

    private async Task<IResponse> CreatePost(CreatePostRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var post = new Post(
                request.Description,
                request.UserId
            );

            _unitOfWork.BeginTransaction();
            await _repository.CreateAsync(post, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new Response<CreatePostResponse>(HttpStatusCode.Created, "Post criado com sucesso",
                new CreatePostResponse(post.Id));
        }
        catch (Exception e)
        {
            _unitOfWork.Rollback();
            throw new Exception($"Falha ao criar post. Detalhes: {e.Message}");
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }
}