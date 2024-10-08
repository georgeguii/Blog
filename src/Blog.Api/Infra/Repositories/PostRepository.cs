﻿using Blog.Api.Domain.Entities;
using Blog.Api.Domain.Interfaces.Repositories;
using Blog.Api.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api.Infra.Repositories;

public class PostRepository(BlogContext context) : IPostRepository
{
    public async Task<IEnumerable<Post>> GetManyAsync(
        int? page = null,
        int? pageSize = null)
    {
        var query = context.Posts.AsQueryable();

        if (page.HasValue && pageSize.HasValue && page > 0 && pageSize > 0)
            query = query.Skip((page.Value - 1) * pageSize.Value)
                .Take(pageSize.Value);

        return await query.ToListAsync();
    }
    
    public async Task<Post?> GetOneAsync(Guid postId)
    {
        return await context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
    }

    public async Task<Post?> GetOneAsync(Guid postId, Guid userId)
    {
        return await context.Posts.FirstOrDefaultAsync(p => p.Id == postId && p.UserId == userId);
    }

    public async Task<int> CountCommentsAsync(Guid id)
    {
        return await context.Posts.Where(x => x.Id == id).Select(x => x.Comments).CountAsync();
    }

    public async Task CreateAsync(Post post, CancellationToken cancellationToken = default)
    {
        await context.Posts.AddAsync(post, cancellationToken);
    }

    public async Task<bool> UpdateAsync(Post post)
    {
        var updated = await context
            .Posts
            .Where(x => x.Id == post.Id)
            .ExecuteUpdateAsync(x => x
                .SetProperty(p => p.Description, post.Description)
                .SetProperty(p => p.LastUpdate, post.LastUpdate));

        return updated != 0;
    }

    public async Task<bool> DeleteAsync(Post post)
    {
        var deleted = await context
            .Posts
            .Where(x => x.Id == post.Id)
            .ExecuteDeleteAsync();

        return deleted != 0;
    }
}