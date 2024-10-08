﻿using System.Text.Json.Serialization;
using Blog.Api.Application.Validators.Post;
using FluentValidation.Results;

namespace Blog.Api.Application.UseCases.Posts.Create;

public class CreatePostRequest
{
    [JsonIgnore]
    public Guid UserId { get; set; }
    public string Description { get; set; }
    
    public ValidationResult Validate()
    {
        var validator = new CreatePostValidator();
        return validator.Validate(this);
    }
}