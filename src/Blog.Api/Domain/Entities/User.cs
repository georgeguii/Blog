using Blog.Api.Domain.ValueObjects;
using Blog.Api.Shared.Entities;
using Blog.Api.Shared.Extensions;

namespace Blog.Api.Domain.Entities;

public sealed class User : Entity
{
    public User()
    {
    }

    public User(
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
        NormalizedName = name.RemoveDiacritics().Trim();
        Document = cpf.Trim();
        IsDisabled = false;
    }

    public Email Email { get; private set; } = string.Empty;
    public Password Password { get; private set; }
    public string Nickname { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string NormalizedName { get; private set; } = string.Empty;
    public Document Document { get; private set; } = string.Empty;

    public bool IsDisabled { get; private set; }

    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public DateTime? LastUpdate { get; private set; }

    public ICollection<Post>? MyPosts { get; private set; } = new List<Post>();

    public ICollection<Comment>? MyComments { get; private set; } = new List<Comment>();


    public ICollection<Like>? Likes { get; private set; } = new List<Like>();

    public void Disable()
    {
        IsDisabled = true;
        LastUpdate = DateTime.Now;
    }

    public void UpdateInfos(string? name = null, string? nickname = null)
    {
        if (name is not null) Name = name.Trim();
        if (nickname is not null)
            nickname.Trim();

        LastUpdate = DateTime.Now;
    }
}