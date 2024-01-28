using Blog.Shared.Entities;
using Blog.ValueObjects.Entities;

namespace Blog.Domain.Entities;

public class User : Entity
{
  public User () { }

  public User (
    Email email,
    Password password,
    string nickname,
    string name,
    string cpf)
  {
    Email = email;
    Password = password;
    Nickname = nickname.Trim();
    Name = name.Trim();
    Cpf = cpf.Trim();
  }

  public Email Email { get; private set; } = string.Empty;
  public Password Password { get; private set; }
  public string Nickname { get; private set; } = string.Empty;
  public string Name { get; private set; } = string.Empty;
  public Cpf Cpf { get; private set; } = string.Empty;

  public DateTime CreatedAt { get; private set; } = DateTime.Now;
  public DateTime? LastUpdate { get; private set; }
}