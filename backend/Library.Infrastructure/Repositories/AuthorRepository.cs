using Library.Application.Interfaces.Repository;
using Library.Domain.Entities;
using Library.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly AppDbContext _context;

    public AuthorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Author>> GetAllAsync() => await _context.Authors.OrderBy(ob => ob.Id).ToListAsync();

    public async Task<IEnumerable<Author>> GetAllWithBooksAsync() => await _context.Authors
                                                                        .OrderBy(a => a.Id)
                                                                        .Include(a => a.Books)
                                                                        .ToListAsync();

    public async Task<Author?> GetByIdAsync(int id) => await _context.Authors.FindAsync(id);

    public async Task AddAsync(Author Author) => await _context.Authors.AddAsync(Author);

    public void Update(Author Author) => _context.Authors.Update(Author);

    public void Delete(Author Author) => _context.Authors.Remove(Author);

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}
