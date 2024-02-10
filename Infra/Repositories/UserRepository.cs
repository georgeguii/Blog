using Blog.Domain.Entities;
using Blog.Infra.Data.Context;
using Blog.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infra.Repositories;

public class UserRepository
{
    private readonly BlogContext _context;
    
    public UserRepository(BlogContext context)
    {
        _context = context;
    }

    public async Task<ICollection<User>> GetMany(
        string? email = null,
        string? nickname = null,
        string? name = null,
        int? page = null,
        int? pageSize = null)
    {
        var query = _context.Users.AsQueryable();

        if (email is not null)
        {
            query = query.Where(x => x.Email.Address.RemoveDiacritics().ToLower().Contains(email.ToLower().RemoveDiacritics()));
        }

        if (nickname is not null)
        {
            query = query.Where(x => x.Nickname.RemoveDiacritics().ToLower().Contains(nickname.ToLower().RemoveDiacritics()));
        }

        if (name is not null)
        {
            query = query.Where(x => x.Name.RemoveDiacritics().ToLower().Contains(name.ToLower().RemoveDiacritics()));
        }

        if (page.HasValue && pageSize.HasValue && page > 0 && pageSize > 0)
        {
            query = query.Skip((page.Value - 1) * pageSize.Value)
                .Take(pageSize.Value);
        }
        return await query.ToListAsync();
    }
}