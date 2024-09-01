using Blog.Domain.ValueObjects;

namespace Blog.Domain.Entities;

public class Topic
{
    public Description Description { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public bool Archived { get; set; }
    public DateTime? LastUpdate { get; private set; }

    public Guid UserId { get; private set; }
    public User? CreatedBy { get; private set; }
}