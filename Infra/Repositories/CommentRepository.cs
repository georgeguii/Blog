using Blog.Domain.Entities;
using Blog.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infra.Repositories;

public class CommentRepository
{
    private readonly BlogContext _context;

    public CommentRepository(BlogContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<object>> GetManyAsync(
    int? page = null,
    int? pageSize = null)
    {
        var query = _context.Comments.AsQueryable();

        if (page.HasValue && pageSize.HasValue && page > 0 && pageSize > 0)
        {
            query = query.Skip((page.Value - 1) * pageSize.Value)
                .Take(pageSize.Value);
        }

        return await query.Select(x => new
        {
            x.Id,
            x.Description,
            x.CreatedBy,
            x.CreatedAt,
            x.LastUpdate,
            numberOfComments = x.ChildrenComments.Count(),
        }).ToListAsync();
    }

    public async Task<Comment?> GetOneAsync(Guid id) =>
        await _context.Comments.FindAsync(id);

    public async Task CreateAsync(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
    }

    public async Task<bool> UpdateAsync(Comment comment)
    {
        var updated = await _context
            .Comments
            .Where(x => x.Id == comment.Id)
            .ExecuteUpdateAsync(x => x
                .SetProperty(p => p.Description, comment.Description)
                .SetProperty(p => p.LastUpdate, comment.LastUpdate));

        return updated != 0;
    }

    public async Task<bool> DeleteAsync(Post post)
    {
        var deleted = await _context
            .Comments
            .Where(x => x.Id == post.Id)
            .ExecuteDeleteAsync();

        return deleted != 0;
    }
}
