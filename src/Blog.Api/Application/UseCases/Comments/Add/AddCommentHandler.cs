using System.Net;
using Blog.Api.Application.Interfaces.Comments;
using Blog.Api.Application.Response;
using Blog.Api.Domain.Entities;
using Blog.Api.Domain.Interfaces;
using Blog.Api.Domain.Interfaces.Repositories;

namespace Blog.Api.Application.UseCases.Comments.Add;

public class AddCommentHandler(
    IUnitOfWork unitOfWork,
    ICommentRepository commentRepository,
    IPostRepository postRepository) : IAddCommentHandler
{
    public async Task<IResponse<AddCommentResponse>> Handle(AddCommentRequest request,
        CancellationToken cancellationToken)
    {
        var requestValidated = request.Validate();

        if (!requestValidated.IsValid)
        {
            var response = new Response<AddCommentResponse>(
                HttpStatusCode.BadRequest,
                "Requisição inválida",
                requestValidated.ToDictionary());
            return response;
        }
        
        var post = await postRepository.GetOneAsync(request.PostId);
        if (post is null)
        {
            var response = new Response<AddCommentResponse>(
                HttpStatusCode.NotFound,
                "Falha ao encontrar o post",
                null);
            return response;
        }

        return await CreateComment(request, post, cancellationToken);
    }

    private async Task<IResponse<AddCommentResponse>> CreateComment(AddCommentRequest request, Post post,
        CancellationToken cancellationToken)
    {
        try
        {
            var comment = new Comment(request.Description);
            post.AddComment(comment);

            unitOfWork.BeginTransaction();
            await commentRepository.CreateAsync(comment, cancellationToken);
            await postRepository.UpdateAsync(post);
            
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