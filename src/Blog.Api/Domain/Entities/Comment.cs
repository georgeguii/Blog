using Blog.Api.Domain.ValueObjects;
using Blog.Api.Shared.Entities;

namespace Blog.Api.Domain.Entities;

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

    public Guid UserId { get; }
    public User CreatedBy { get; }

    public Guid PostId { get; }
    public Post OnPost { get; }

    public Guid? CommentId { get; }
    public Comment RelatedComment { get; }

    public ICollection<Comment> ChildrenComments { get; private set; } = new List<Comment>();
    
    public void UpdateDescription(string description)
    {
        Description = description;
        LastUpdate = DateTime.Now;
    }

}
