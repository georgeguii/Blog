using Blog.Domain.Services;
using Blog.Shared.Exceptions;

namespace Blog.ValueObjects.Entities;

public sealed class Password
{
  public Password() { }
  public Password(string hash)
  {
      Hash = hash.Trim().Encrypt();
  }

  public string? Hash { get; private set; }

  public bool Verify(string password)
    => Criptography.CompareHash(password, Hash!);

  public void UpdatePassword(string password)
  {
    InvalidParametersException.ThrowIfNull(password, "Senha inválida.");
    Hash = password.Trim().Encrypt();
  }
}