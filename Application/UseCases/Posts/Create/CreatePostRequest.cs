namespace Blog.Application.UseCases.Posts.Create;

public class CreatePostRequest
{
    public Guid UserId { get; set; }
    public string Description { get; set; }
}
