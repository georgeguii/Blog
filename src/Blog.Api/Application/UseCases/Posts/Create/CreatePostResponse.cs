namespace Blog.Api.Application.UseCases.Posts.Create;

public class CreatePostResponse
{
    public Guid Id { get; set; }

    public CreatePostResponse() { }
    
    public CreatePostResponse(Guid id)
    {
        Id = id;
    }
}