using Library.Application.DTOs.Genre;
using Library.Application.Responses;
using Library.Application.ViewModels;

namespace Library.Application.Interfaces.Services;

public interface IGenreService
{
    Task<BaseResponse<IEnumerable<GenreDto>>> GetAllAsync();
    Task<BaseResponse<IEnumerable<GenreViewModel>>> GetAllDetailedAsync();
    Task<BaseResponse<GenreDto>> GetByIdAsync(int id);
    Task<BaseResponse<GenreDto>> CreateAsync(CreateGenreDto dto);
    Task<BaseResponse<bool>> UpdateAsync(int id, UpdateGenreDto dto);
    Task<BaseResponse<bool>> DeleteAsync(int id);
}
