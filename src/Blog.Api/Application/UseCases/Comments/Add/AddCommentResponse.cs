namespace Blog.Api.Application.UseCases.Comments.Add;

public class AddCommentResponse
{
    public Guid Id { get; set; }

    public AddCommentResponse()
    {
    }

    public AddCommentResponse(Guid id)
    {
        Id = id;
    }
}