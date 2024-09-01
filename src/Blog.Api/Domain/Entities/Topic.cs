using Blog.Api.Domain.ValueObjects;

namespace Blog.Api.Domain.Entities;

public class Topic
{
    public Description Description { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public bool Archived { get; set; }
    public DateTime? LastUpdate { get; }

    public Guid UserId { get; }
    public User? CreatedBy { get; }
}