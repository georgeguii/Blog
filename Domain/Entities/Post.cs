using Blog.Shared.Entities;
using Blog.ValueObjects.Entities;

namespace Blog.Domain.Entities;

public sealed class Post : Entity
{
    public Post () { }

    public Post (string description)
    {
    Description = description;
    }

    public Description Description { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public DateTime? LastUpdate { get; private set; }

    public Guid UserId { get; private set; }
    public User CreatedBy { get; private set; }


    public ICollection<Comment>? Comments { get; private set; } = new List<Comment>();
    
    public ICollection<Like>? Likes { get; private set; } = new List<Like>();
}