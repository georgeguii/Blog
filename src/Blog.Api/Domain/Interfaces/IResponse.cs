using System.Net;

namespace Blog.Api.Domain.Interfaces;

public interface IResponse
{
    HttpStatusCode StatusCode { get; set; }
    string Message { get; set; }
    IDictionary<string, string[]> Errors { get; set; }
}