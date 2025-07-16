using Library.Application.DTOs.Author;
using Library.Application.Responses;
using Library.Application.ViewModels;

namespace Library.Application.Interfaces.Services;

public interface IAuthorService
{
    Task<BaseResponse<IEnumerable<AuthorDto>>> GetAllAsync();
    Task<BaseResponse<IEnumerable<AuthorViewModel>>> GetAllDetailedAsync();
    Task<BaseResponse<AuthorDto>> GetByIdAsync(int id);
    Task<BaseResponse<AuthorDto>> CreateAsync(CreateAuthorDto dto);
    Task<BaseResponse<bool>> UpdateAsync(int id, UpdateAuthorDto dto);
    Task<BaseResponse<bool>> DeleteAsync(int id);
}
