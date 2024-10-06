using Blog.Api.Domain.Services;
using Blog.Api.Shared.Exceptions;

namespace Blog.Api.Domain.ValueObjects;

public sealed class Password
{
    public string? Hash { get; private set; }

    public Password()
    {
    }

    public Password(string hash)
    {
        Hash = hash.Trim().Encrypt();
    }
    
    public bool Verify(string password)
    {
        return Criptography.CompareHash(password, Hash!);
    }

    public void UpdatePassword(string password)
    {
        InvalidParametersException.ThrowIfNull(password, "Senha inválida.");
        Hash = password.Trim().Encrypt();
    }
}