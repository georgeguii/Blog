﻿using Blog.Domain.Entities;
using Blog.Domain.Interfaces.Repositories;
using Blog.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infra.Repositories;

public class LikeRepository(BlogContext context) : ILikeRepository
{
    public async Task<IEnumerable<Like>> GetManyAsync(
        int? page = null,
        int? pageSize = null)
    {
        var query = context.Likes.AsQueryable();

        if (page.HasValue && pageSize.HasValue && page > 0 && pageSize > 0)
        {
            query = query.Skip((page.Value - 1) * pageSize.Value)
                .Take(pageSize.Value);
        }

        return await query.ToListAsync();
    }

    public async Task CreateAsync(Like like)
    {
        await context.Likes.AddAsync(like);
    }

    public async Task<bool> DeleteAsync(Like like)
    {
        var deleted = await context
            .Likes
            .Where(x => x.UserId == like.UserId && x.PostId == like.PostId)
            .ExecuteDeleteAsync();

        return deleted != 0;
    }
}