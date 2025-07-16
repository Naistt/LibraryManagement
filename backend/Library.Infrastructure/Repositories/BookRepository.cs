using Library.Application.Interfaces.Repository;
using Library.Domain.Entities;
using Library.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;

    public BookRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book>> GetAllAsync() => await _context.Books.OrderBy(ob => ob.Id).ToListAsync();
    public async Task<IEnumerable<Book>> GetAllWithDetailsAsync() => await _context.Books
                                                                        .OrderBy(ob => ob.Id)
                                                                        .Include(b => b.Author)
                                                                        .Include(b => b.Genre)
                                                                        .ToListAsync();

    public async Task<Book?> GetByIdAsync(int id) => await _context.Books.FindAsync(id);

    public async Task AddAsync(Book book) => await _context.Books.AddAsync(book);

    public void Update(Book book) => _context.Books.Update(book);

    public void Delete(Book book) => _context.Books.Remove(book);

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}
