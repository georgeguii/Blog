namespace Blog.Api.Application.UseCases.Users.Create;

public class CreateUserResponse
{
    public Guid Id { get; set; }

    public CreateUserResponse() { }
    
    public CreateUserResponse(Guid id)
    {
        Id = id;
    }
}