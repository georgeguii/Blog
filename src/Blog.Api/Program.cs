using Blog.Api.Domain.Interfaces.Repositories;
using Blog.Api.Infra.Data.Context;
using Blog.Api.Infra.Data.Extensions;
using Blog.Api.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ILikeRepository, LikeRepository>();

builder.Services.AddStackExchangeRedisCache(o =>
{
    var connectionStringRedis = builder.Configuration.GetConnectionString("Cache");
    o.InstanceName = "blog";
    o.Configuration = connectionStringRedis;
});

builder.Services.AddDbContextPool<BlogContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseHealthChecks("/health");
app.Run();