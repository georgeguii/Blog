using System.Net;
using Blog.Api.Application.Interfaces.Users;
using Blog.Api.Application.Response;
using Blog.Api.Domain.Interfaces;
using Blog.Api.Domain.Interfaces.Repositories;

namespace Blog.Api.Application.UseCases.Users.UpdateInfos;

public class UpdateUserHandler(IUnitOfWork unitOfWork, IUserRepository repository) : IUpdateInfosUserHandler
{
    public async Task<IResponse<string>> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var requestValidated = request.Validate();

        if (!requestValidated.IsValid)
        {
            return new Response<string>(
                HttpStatusCode.BadRequest,
                "Requisição inválida",
                requestValidated.ToDictionary());
        }

        return await UpdateUserInfos(request, cancellationToken);
    }

    private async Task<IResponse<string>> UpdateUserInfos(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await repository.GetOneAsync(request.UserId);
            if (user == null)
            {
                return new Response<string>(HttpStatusCode.NotFound, null, "Usuário não encontrado.");
            }

            if (!string.IsNullOrWhiteSpace(request.Nickname))
            {
                var existingUser = await repository.GetOneAsync(request.Nickname);
                if (existingUser != null && existingUser.Id != user.Id)
                {
                    return new Response<string>(
                        HttpStatusCode.Conflict,
                        null,
                        "Este nickname já está em uso.");
                }
                user.UpdateInfos(nickname: request.Nickname);
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                user.UpdateInfos(name: request.Name);
            }

            await repository.UpdateAsync(user);
            await unitOfWork.CommitAsync(cancellationToken);

            return new Response<string>(HttpStatusCode.NoContent, null, "Informações do usuário atualizadas com sucesso.");
        }
        catch (Exception e)
        {
            unitOfWork.Rollback();
            throw new Exception($"Falha ao atualizar informações do usuário. Detalhes: {e.Message}");
        }
        finally
        {
            unitOfWork.Dispose();
        }
    }
}