using Library.Application.DTOs.Genre;
using Library.Application.Interfaces.Repository;
using Library.Application.Interfaces.Services;
using Library.Application.Responses;
using Library.Application.ViewModels;
using Library.Domain.Entities;

namespace Library.Application.Services;
public class GenreService : IGenreService
{
    private readonly IGenreRepository _genreRepository;

    public GenreService(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<BaseResponse<IEnumerable<GenreDto>>> GetAllAsync()
    {
        var genres = await _genreRepository.GetAllAsync();
        var result = genres.Select(g => new GenreDto
        {
            Id = g.Id,
            Name = g.Name,
            Description = g.Description
        });

        return new BaseResponse<IEnumerable<GenreDto>>(result);
    }

    public async Task<BaseResponse<IEnumerable<GenreViewModel>>> GetAllDetailedAsync()
    {
        var genres = await _genreRepository.GetAllWithBooksAsync();

        var result = genres.Select(g => new GenreViewModel
        {
            Id = g.Id,
            Name = g.Name,
            Description = g.Description,
            BookTitles = g.Books.Select(b => b.Title).ToList()
        });

        return new BaseResponse<IEnumerable<GenreViewModel>>(result);
    }

    public async Task<BaseResponse<GenreDto>> GetByIdAsync(int id)
    {
        var genre = await _genreRepository.GetByIdAsync(id);
        if (genre == null)
            return new BaseResponse<GenreDto>("Gênero não encontrado.");

        return new BaseResponse<GenreDto>(new GenreDto
        {
            Id = genre.Id,
            Name = genre.Name,
            Description = genre.Description
        });
    }

    public async Task<BaseResponse<GenreDto>> CreateAsync(CreateGenreDto dto)
    {
        var genre = new Genre
        {
            Name = dto.Name,
            Description = dto.Description
        };

        await _genreRepository.AddAsync(genre);
        await _genreRepository.SaveChangesAsync();


        return new BaseResponse<GenreDto>(new GenreDto
        {
            Id = genre.Id,
            Name = genre.Name,
            Description = genre.Description
        }, "Gênero criado com sucesso.");
    }

    public async Task<BaseResponse<bool>> UpdateAsync(int id, UpdateGenreDto dto)
    {
        var genre = await _genreRepository.GetByIdAsync(id);
        if (genre == null)
            return new BaseResponse<bool>("Gênero não encontrado.");

        genre.Name = dto.Name;
        genre.Description = dto.Description;

        _genreRepository.Update(genre);
        await _genreRepository.SaveChangesAsync();

        return new BaseResponse<bool>(true, "Gênero atualizado com sucesso.");
    }

    public async Task<BaseResponse<bool>> DeleteAsync(int id)
    {
        var existing = await _genreRepository.GetByIdAsync(id);
        if (existing == null)
            return new BaseResponse<bool>("Gênero não encontrado.");

        _genreRepository.Delete(existing);
        await _genreRepository.SaveChangesAsync();

        return new BaseResponse<bool>(true, "Gênero removido com sucesso.");
    }
}
