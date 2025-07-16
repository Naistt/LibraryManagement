using Library.Domain.Entities;

namespace Library.Application.Interfaces.Repository;

public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAllAsync();
    Task<IEnumerable<Author>> GetAllWithBooksAsync();
    Task<Author?> GetByIdAsync(int id);
    Task AddAsync(Author author);
    void Update(Author author);
    void Delete(Author author);
    Task SaveChangesAsync();
}
