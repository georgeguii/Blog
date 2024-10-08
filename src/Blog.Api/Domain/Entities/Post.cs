using Blog.Api.Domain.ValueObjects;
using Blog.Api.Shared.Entities;

namespace Blog.Api.Domain.Entities;

public sealed class Post : Entity
{
    public Post()
    {
    }

    public Post(string description, Guid userId)
    {
        UserId = userId;
        Description = description;
        CreatedAt = DateTime.UtcNow;
        Archived = false;
    }

    public Description Description { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public bool Archived { get; set; }
    public DateTime? LastUpdate { get; private set; }

    public Guid UserId { get; }
    public User CreatedBy { get; }


    public int CommentCount { get; private set; }
    public ICollection<Comment>? Comments { get; private set; } = new List<Comment>();


    public int LikesCount { get; private set; }
    public ICollection<Like>? Likes { get; private set; } = new List<Like>();

    public void UpdateDescription(string description)
    {
        Description = description;
        LastUpdate = DateTime.Now;
    }

    public void Archive()
    {
        Archived = true;
        LastUpdate = DateTime.Now;
    }

    public void Unarchive()
    {
        Archived = false;
        LastUpdate = DateTime.Now;
    }

    public void AddComment(Comment comment)
    {
        if (Comments == null) Comments = new List<Comment>();

        CommentCount += 1;
        Comments.Add(comment);
    }

    public void AddLike(Like like)
    {
        Likes ??= new List<Like>();
        LikesCount += 1;
        Likes.Add(like);
    }

    public void RemoveLike(Like like)
    {
        if (Likes == null) throw new InvalidOperationException("Não é possível remover uma curtida que não existe");

        LikesCount -= 1;
        Likes.Remove(like);
    }
}