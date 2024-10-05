using Blog.Api.Domain.Entities;
using Blog.Api.Domain.Interfaces.Repositories;
using Blog.Api.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api.Infra.Repositories;

public class CommentRepository(BlogContext context) : ICommentRepository
{
    public async Task<IEnumerable<Comment>> GetManyAsync(
        int? page = null,
        int? pageSize = null)
    {
        var query = context.Comments.AsQueryable();

        if (page.HasValue && pageSize.HasValue && page > 0 && pageSize > 0)
            query = query.Skip((page.Value - 1) * pageSize.Value)
                .Take(pageSize.Value);

        return await query.ToListAsync();
    }

    public async Task<Comment?> GetOneAsync(Guid commentId, Guid userId)
    {
        return await context.Comments.FirstOrDefaultAsync(c => c.CommentId == commentId&& c.UserId == userId);
    }

    public async Task CreateAsync(Comment comment, CancellationToken cancellationToken)
    {
        await context.Comments.AddAsync(comment, cancellationToken);
    }

    public async Task<bool> UpdateAsync(Comment comment)
    {
        var updated = await context
            .Comments
            .Where(x => x.Id == comment.Id)
            .ExecuteUpdateAsync(x => x
                .SetProperty(p => p.Description, comment.Description)
                .SetProperty(p => p.LastUpdate, comment.LastUpdate));

        return updated != 0;
    }

    public async Task<bool> DeleteAsync(Comment comment)
    {
        var deleted = await context
            .Comments
            .Where(x => x.Id == comment.Id)
            .ExecuteDeleteAsync();

        return deleted != 0;
    }
}