using Library.Application.DTOs.Author;
using Library.Application.Interfaces.Repository;
using Library.Application.Interfaces.Services;
using Library.Application.Responses;
using Library.Application.ViewModels;
using Library.Domain.Entities;

namespace Library.Application.Services;
public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<BaseResponse<IEnumerable<AuthorDto>>> GetAllAsync()
    {
        var authors = await _authorRepository.GetAllAsync();
        var result = authors.Select(a => new AuthorDto
        {
            Id = a.Id,
            Name = a.Name,
            Bio = a.Bio
        });

        return new BaseResponse<IEnumerable<AuthorDto>>(result);
    }

    public async Task<BaseResponse<IEnumerable<AuthorViewModel>>> GetAllDetailedAsync()
    {
        var authors = await _authorRepository.GetAllWithBooksAsync();

        var result = authors.Select(a => new AuthorViewModel
        {
            Id = a.Id,
            Name = a.Name,
            Bio = a.Bio,
            BookTitles = a.Books.Select(b => b.Title).ToList()
        });

        return new BaseResponse<IEnumerable<AuthorViewModel>>(result);
    }


    public async Task<BaseResponse<AuthorDto>> GetByIdAsync(int id)
    {
        var author = await _authorRepository.GetByIdAsync(id);
        if (author == null)
            return new BaseResponse<AuthorDto>("Autor não encontrado.");

        return new BaseResponse<AuthorDto>(new AuthorDto
        {
            Id = author.Id,
            Name = author.Name,
            Bio = author.Bio
        });
    }

    public async Task<BaseResponse<AuthorDto>> CreateAsync(CreateAuthorDto dto)
    {
        var author = new Author
        {
            Name = dto.Name,
            Bio = dto.Bio
        };

        await _authorRepository.AddAsync(author);
        await _authorRepository.SaveChangesAsync();

        return new BaseResponse<AuthorDto>(new AuthorDto
        {
            Id = author.Id,
            Name = author.Name,
            Bio = author.Bio
        }, "Autor criado com sucesso.");
    }

    public async Task<BaseResponse<bool>> UpdateAsync(int id, UpdateAuthorDto dto)
    {
        var author = await _authorRepository.GetByIdAsync(id);
        if (author == null)
            return new BaseResponse<bool>("Autor não encontrado.");

        author.Name = dto.Name;
        author.Bio = dto.Bio;

        _authorRepository.Update(author);
        await _authorRepository.SaveChangesAsync();

        return new BaseResponse<bool>(true, "Autor atualizado com sucesso.");
    }

    public async Task<BaseResponse<bool>> DeleteAsync(int id)
    {
        var author = await _authorRepository.GetByIdAsync(id);
        if (author == null)
            return new BaseResponse<bool>("Autor não encontrado.");

        _authorRepository.Delete(author);
        await _authorRepository.SaveChangesAsync();

        return new BaseResponse<bool>(true, "Autor removido com sucesso.");
    }
}
