using Library.Application.DTOs.Book;
using Library.Application.Interfaces.Repository;
using Library.Application.Interfaces.Services;
using Library.Application.Responses;
using Library.Application.ViewModels;
using Library.Domain.Entities;

namespace Library.Application.Services;
public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<BaseResponse<IEnumerable<BookDto>>> GetAllAsync()
    {
        var books = await _bookRepository.GetAllAsync();
        var result = books.Select(b => new BookDto
        {
            Id = b.Id,
            Title = b.Title,
            Summary = b.Summary,
            PublicationYear = b.PublicationYear,
            AuthorId = b.AuthorId,
            GenreId = b.GenreId,
        });

        return new BaseResponse<IEnumerable<BookDto>>(result);
    }

    public async Task<IEnumerable<BookViewModel>> GetAllDetailedAsync()
    {
        var books = await _bookRepository.GetAllWithDetailsAsync();

        return books.Select(b => new BookViewModel
        {
            Id = b.Id,
            Title = b.Title,
            Summary = b.Summary,
            PublicationYear = b.PublicationYear,
            AuthorId = b.AuthorId,
            AuthorName = b.Author != null ? b.Author.Name : string.Empty,
            GenreId = b.GenreId,
            GenreName = b.Genre != null ? b.Genre.Name : string.Empty
        });
    }

    public async Task<BaseResponse<BookDto>> GetByIdAsync(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null)
            return new BaseResponse<BookDto>("Livro não encontrado.");

        return new BaseResponse<BookDto>(new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Summary = book.Summary,
            PublicationYear = book.PublicationYear,
            AuthorId = book.AuthorId,
            GenreId = book.GenreId,
        });
    }

    public async Task<BaseResponse<BookDto>> CreateAsync(CreateBookDto dto)
    {
        var book = new Book
        {
            Title = dto.Title,
            Summary = dto.Summary,
            PublicationYear = dto.PublicationYear,
            AuthorId = dto.AuthorId,
            GenreId = dto.GenreId,
        };

        await _bookRepository.AddAsync(book);
        await _bookRepository.SaveChangesAsync();

        return new BaseResponse<BookDto>(new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Summary = book.Summary,
            PublicationYear = book.PublicationYear,
            AuthorId = book.AuthorId,
            GenreId = book.GenreId,
        }, "Livro criado com sucesso.");
    }

    public async Task<BaseResponse<bool>> UpdateAsync(int id, UpdateBookDto dto)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null)
            return new BaseResponse<bool>("Livro não encontrado.");

        book.Title = dto.Title;
        book.Summary = dto.Summary;
        book.PublicationYear = dto.PublicationYear;
        book.AuthorId = dto.AuthorId;
        book.GenreId = dto.GenreId;

        _bookRepository.Update(book);
        await _bookRepository.SaveChangesAsync();

        return new BaseResponse<bool>(true, "Livro atualizado com sucesso.");
    }

    public async Task<BaseResponse<bool>> DeleteAsync(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null)
            return new BaseResponse<bool>("Livro não encontrado.");

        _bookRepository.Delete(book);
        await _bookRepository.SaveChangesAsync();

        return new BaseResponse<bool>(true, "Livro removido com sucesso.");
    }
}
