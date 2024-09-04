using System.Net;
using Blog.Api.Domain.Interfaces;

namespace Blog.Api.Application.Response;

public class Response<TData> : IResponse where TData : class
{
    public Response()
    {
    }

    public Response(TData data, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        StatusCode = statusCode;
        Data = data;
    }
    
    public Response(HttpStatusCode statusCode, string message, IDictionary<string, string[]> errors)
    {
        StatusCode = statusCode;
        Message = message;
        Errors = errors;
    }

    public Response(HttpStatusCode statusCode, string message, TData data)
    {
        StatusCode = statusCode;
        Message = message;
        Data = data;
    }

    public TData? Data { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }
    public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
}