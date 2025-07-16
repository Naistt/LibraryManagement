using Library.Application.DTOs.Book;
using Library.Application.Interfaces.Repository;
using Library.Application.Responses;
using Library.Application.ViewModels;

namespace Library.Application.Interfaces.Services;

public interface IBookService
{
    Task<BaseResponse<IEnumerable<BookDto>>> GetAllAsync();
    Task<IEnumerable<BookViewModel>> GetAllDetailedAsync();
    Task<BaseResponse<BookDto>> GetByIdAsync(int id);
    Task<BaseResponse<BookDto>> CreateAsync(CreateBookDto dto);
    Task<BaseResponse<bool>> UpdateAsync(int id, UpdateBookDto dto);
    Task<BaseResponse<bool>> DeleteAsync(int id);
}
