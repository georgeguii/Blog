using Blog.Domain.Entities;
using Blog.Domain.Interfaces.Repositories;
using Blog.Infra.Data.Context;
using Blog.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infra.Repositories;

public class UserRepository(BlogContext context) : IUserRepository
{
    public async Task<IEnumerable<User>> GetManyAsync(
        string? email = null,
        string? nickname = null,
        string? name = null,
        int? page = null,
        int? pageSize = null)
    {
        var query = context.Users.AsQueryable();

        if (email is not null)
        {
            query = query.Where(x => x.Email.Address.Contains(email, StringComparison.CurrentCultureIgnoreCase));
        }

        if (nickname is not null)
        {
            query = query.Where(x => x.Nickname.Contains(nickname, StringComparison.CurrentCultureIgnoreCase));
        }

        if (name is not null)
        {
            query = query.Where(x => x.NormalizedName.ToLower().Contains(name.ToLower().RemoveDiacritics()));
        }

        if (page.HasValue && pageSize.HasValue && page > 0 && pageSize > 0)
        {
            query = query.Skip((page.Value - 1) * pageSize.Value)
                .Take(pageSize.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<User?> GetOneAsync(Guid id) =>
        await context.Users.FindAsync(id);

    public async Task CreateAsync(User user)
    {
        await context.Users.AddAsync(user);
    }

    public async Task<bool> UpdateAsync(User user)
    {
        var updated = await context
            .Users
            .Where(x => x.Id == user.Id)
            .ExecuteUpdateAsync(x => x
                .SetProperty(u => u.Name, user.Name)
                .SetProperty(u => u.Nickname, user.Nickname)
                .SetProperty(u => u.LastUpdate, user.LastUpdate));

        return updated != 0;
    }

    public async Task<bool> DisableAsync(User user)
    {
        var updated = await context
            .Users
            .Where(x => x.Id == user.Id)
            .ExecuteUpdateAsync(x => x
                .SetProperty(u => u.IsDisabled, user.IsDisabled)
                .SetProperty(u => u.LastUpdate, user.LastUpdate));

        return updated != 0;
    }
}