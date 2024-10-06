using System.Net;

namespace Blog.Api.Domain.Interfaces;

public interface IResponse<TData>
{
    HttpStatusCode StatusCode { get; set; }
    string Message { get; set; }
    public TData? Data { get; set; }
    IDictionary<string, string[]> Errors { get; set; }
}