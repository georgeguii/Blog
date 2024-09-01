﻿using System.Net;
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

    public Response(HttpStatusCode statusCode, string message, TData data)
    {
        StatusCode = statusCode;
        Message = message;
        Data = data;
    }

    public TData? Data { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }
    public Dictionary<string, string> Errors { get; set; } = new();

    public void AddError(string key, string value)
    {
        Errors.Add(key, value);
    }
}