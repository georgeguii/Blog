namespace Blog.Application.Request;

public class PagedRequest : AuthorizedRequest
{
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
}