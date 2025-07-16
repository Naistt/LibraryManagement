using Library.Domain.Entities;

namespace Library.Application.Interfaces.Repository;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllAsync();
    Task<IEnumerable<Book>> GetAllWithDetailsAsync();
    Task<Book?> GetByIdAsync(int id);
    Task AddAsync(Book book);
    void Update(Book book);
    void Delete(Book book);
    Task SaveChangesAsync();
}
