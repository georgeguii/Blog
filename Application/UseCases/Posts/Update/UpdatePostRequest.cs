namespace Blog.Application.UseCases.Posts.Update;

public class UpdatePostRequest
{
    public Guid Id { get; set; }
    public string Description { get; set; }
}
