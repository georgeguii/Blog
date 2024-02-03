using Blog.Shared.Entities;
using Blog.ValueObjects.Entities;

namespace Blog.Domain.Entities;

public sealed class User : Entity
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
        IsDisabled = false;
    }

    public Email Email { get; private set; } = string.Empty;
    public Password Password { get; private set; }
    public string Nickname { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public Cpf Cpf { get; private set; } = string.Empty;

    public bool IsDisabled { get; private set; } = false;

    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public DateTime? LastUpdate { get; private set; }

    public ICollection<Post> MyPosts { get; private set;}

    public ICollection<Comment> MyComments { get; private set; }
}