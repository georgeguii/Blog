using Blog.Domain.ValueObjects;
using Blog.Shared.Entities;

namespace Blog.Domain.Entities;

public sealed class Comment : Entity
{
    public Comment()
    {
    }

    public Comment(string description)
    {
        Description = description;
    }

    public Description Description { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public DateTime? LastUpdate { get; private set; }

    public Guid UserId { get; private set; }
    public User CreatedBy { get; private set; }

    public Guid PostId { get; private set; }
    public Post OnPost { get; private set; }

    public Guid? CommentId { get; private set; }
    public Comment RelatedComment { get; private set; }

    public ICollection<Comment> ChildrenComments { get; private set; } = new List<Comment>();
}