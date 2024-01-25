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
  public string? Code { get; private set; }
  public DateTime? ExpireDate { get; private set; }
  public DateTime? ActivateDate { get; private set; } = null;
  public bool Active => ActivateDate != null && ExpireDate == null;

  public void GenerateCode()
  {
    Code = Guid.NewGuid().ToString("N")[..8].ToUpper();
    ExpireDate = DateTime.Now.AddMinutes(5);
    ActivateDate = null;
  }

  public bool Verify(string password)
    => Criptography.CompareHash(password, Hash!);

  public void UpdatePassword(string password)
  {
    InvalidParametersException.ThrowIfNull(password, "Senha inválida.");
    Hash = password.Trim().Encrypt();
  }
}