using System.Net;
using Blog.Api.Application.Interfaces.Users;
using Blog.Api.Application.Response;
using Blog.Api.Domain.Entities;
using Blog.Api.Domain.Interfaces;
using Blog.Api.Domain.Interfaces.Repositories;
using Blog.Api.Domain.ValueObjects;

namespace Blog.Api.Application.UseCases.Users.Create;

public class CreateUserHandler(IUnitOfWork unitOfWork, IUserRepository repository) : ICreateUserHandler
{
    public async Task<IResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var requestValidated = request.Validate();

        if (!requestValidated.IsValid)
        {
            return new Response<string>(
                HttpStatusCode.BadRequest,
                "Requisição inválida",
                requestValidated.ToDictionary());
        }

        var userInfosRegistered = await ValidateUser(request);

        if (userInfosRegistered is not null)
            return userInfosRegistered;
        
        return await CreateUser(request, cancellationToken);
    }

    private async Task<Response<string>?> ValidateUser(CreateUserRequest request)
    {
        if (await repository.CheckIfEmailIsAlreadyRegistered(request.Email))
        {
            return new Response<string>(HttpStatusCode.Conflict, null, "Email já está em uso.");
        }

        if (await repository.CheckIfNicknameIsAlreadyRegistered(request.Nickname))
        {
            return new Response<string>(HttpStatusCode.Conflict, null, "Nickname já está em uso.");
        }

        if (await repository.CheckIfDocumentIsAlreadyRegistered(request.Document))
        {
            return new Response<string>(HttpStatusCode.Conflict, null, "Documento já cadastrado.");
        }

        return null;
    }

    private async Task<Response<CreateUserResponse>> CreateUser(CreateUserRequest request,
        CancellationToken cancellationToken)
    {
        var user = new User(
            new Email(request.Email),
            new Password(request.Password),
            request.Nickname,
            request.Name,
            request.Document);

        try
        {
            unitOfWork.BeginTransaction();

            await repository.CreateAsync(user);
            await unitOfWork.CommitAsync(cancellationToken);

            return new Response<CreateUserResponse>(HttpStatusCode.Created, new CreateUserResponse(user.Id),
                "Usuário criado com sucesso.");
        }
        catch (Exception e)
        {
            unitOfWork.Rollback();
            throw new Exception($"Falha ao criar usuário. Detalhes: {e.Message}");
        }
        finally
        {
            unitOfWork.Dispose();
        }
    }
}