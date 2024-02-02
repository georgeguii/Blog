namespace Blog.Domain.Entities;

public sealed class Like
{
  public Like () { }

  public Like(Guid userId, Guid postId)
  {
    UserId = userId;
    PostId = postId;
  }

  public Guid UserId { get; private set; }
  public Guid PostId { get; private set;}

  public DateTime CreatedAt { get; private set; } = DateTime.Now;
}