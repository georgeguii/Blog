using Blog.Domain.Entities;
using Blog.Domain.Interfaces.Repositories;
using Blog.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infra.Repositories;

public class LikeRepository : ILikeRepository
{
    private readonly BlogContext _context;

    public LikeRepository(BlogContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<object>> GetManyAsync(
    int? page = null,
    int? pageSize = null)
    {
        var query = _context.Likes.AsQueryable();

        if (page.HasValue && pageSize.HasValue && page > 0 && pageSize > 0)
        {
            query = query.Skip((page.Value - 1) * pageSize.Value)
                .Take(pageSize.Value);
        }

        return await query.Select(x => new
        {
            x.UserId,
            x.PostId
        }).ToListAsync();
    }

    public async Task CreateAsync(Like like)
    {
        await _context.Likes.AddAsync(like);
    }

    public async Task<bool> DeleteAsync(Like like)
    {
        var deleted = await _context
            .Likes
            .Where(x => x.UserId == like.UserId && x.PostId == like.PostId)
            .ExecuteDeleteAsync();

        return deleted != 0;
    }
}
