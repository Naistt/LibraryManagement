using Library.Application.Interfaces.Repository;
using Library.Domain.Entities;
using Library.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly AppDbContext _context;

    public GenreRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Genre>> GetAllAsync() => await _context.Genres.OrderBy(ob => ob.Id).ToListAsync();

    public async Task<IEnumerable<Genre>> GetAllWithBooksAsync() => await _context.Genres
                                                                        .OrderBy(ob => ob.Id)
                                                                        .Include(g => g.Books)
                                                                        .ToListAsync();

    public async Task<Genre?> GetByIdAsync(int id) => await _context.Genres.FindAsync(id);

    public async Task AddAsync(Genre genre) => await _context.Genres.AddAsync(genre);

    public void Update(Genre genre) => _context.Genres.Update(genre);

    public void Delete(Genre genre) => _context.Genres.Remove(genre);

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

}
