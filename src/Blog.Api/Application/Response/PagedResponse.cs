using Blog.Api.Domain;

namespace Blog.Api.Application.Response;

public class PagedResponse<TData> : Response<TData> where TData : class
{
    public PagedResponse(
        int statusCode = Configuration.DefaultStatusCode,
        string? message = null,
        TData? data = null)
    {
    }

    public PagedResponse(
        int totalCount,
        int currentPage = 1,
        int pageSize = Configuration.DefaultPageSize,
        TData? data = null) : base(data)
    {
        Data = data;
        TotalCount = totalCount;
        CurrentPage = currentPage;
        PageSize = pageSize;
    }

    public int CurrentPage { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public int PageSize { get; set; } = Configuration.DefaultPageSize;
    public int TotalCount { get; set; }
}