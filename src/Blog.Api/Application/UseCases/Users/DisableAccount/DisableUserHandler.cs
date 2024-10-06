using System.Net;
using Blog.Api.Application.Interfaces.Users;
using Blog.Api.Application.Response;
using Blog.Api.Domain.Interfaces;
using Blog.Api.Domain.Interfaces.Repositories;

namespace Blog.Api.Application.UseCases.Users.DisableAccount;

public class DisableUserHandler(IUnitOfWork unitOfWork, IUserRepository repository) : IDisableUserHandler
{
    public async Task<IResponse> Handle(DisableUserRequest request, CancellationToken cancellationToken)
    {
        var requestValidated = request.Validate();

        if (!requestValidated.IsValid)
        {
            return new Response<string>(
                HttpStatusCode.BadRequest,
                "Requisição inválida",
                requestValidated.ToDictionary());
        }
        
        return await DisableUser(request, cancellationToken);
    }

    private async Task<IResponse> DisableUser(DisableUserRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await repository.GetOneAsync(request.UserId);
            if (user == null)
            {
                return new Response<string>(HttpStatusCode.NotFound, null, "Usuário não encontrado.");
            }

            user.Disable();

            await repository.DisableAsync(user);
            await unitOfWork.CommitAsync(cancellationToken);

            return new Response<string>(HttpStatusCode.NoContent, null, "Conta desativada com sucesso.");
        }
        catch (Exception e)
        {
            unitOfWork.Rollback();
            throw new Exception($"Falha ao desativar conta. Detalhes: {e.Message}");
        }
        finally
        {
            unitOfWork.Dispose();
        }

    }
}