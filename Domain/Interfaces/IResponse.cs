using System.Net;

namespace Blog.Domain.Interfaces;

public interface IResponse
{
    HttpStatusCode StatusCode { get; set; }
    string Message { get; set; }
    Dictionary<string, string> Errors { get; set; }
}