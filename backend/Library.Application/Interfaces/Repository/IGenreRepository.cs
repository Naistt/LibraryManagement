using Library.Domain.Entities;

namespace Library.Application.Interfaces.Repository;

public interface IGenreRepository
{
    Task<IEnumerable<Genre>> GetAllAsync();
    Task<IEnumerable<Genre>> GetAllWithBooksAsync();
    Task<Genre?> GetByIdAsync(int id);
    Task AddAsync(Genre genre);
    void Update(Genre genre);
    void Delete(Genre genre);
    Task SaveChangesAsync();
}
