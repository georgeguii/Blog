﻿using Blog.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api.Infra.Data.Context;

public class BlogContext : DbContext
{
    public BlogContext(DbContextOptions<BlogContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Post> Posts { get; set; }

    public DbSet<Comment> Comments { get; set; }

    public DbSet<Like> Likes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlogContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}