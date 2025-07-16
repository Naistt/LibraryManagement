using Microsoft.AspNetCore.Mvc;
using Library.Application.DTOs.Book;
using Library.Application.Interfaces.Services;
using Library.Application.Responses;
using Library.Application.ViewModels;

namespace Library.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<IEnumerable<BookDto>>>> GetAll()
    {
        var response = await _bookService.GetAllAsync();
        return Ok(response);
    }

    [HttpGet("detailed")]
    public async Task<IActionResult> GetDetailed()
    {
        var result = await _bookService.GetAllDetailedAsync();
        return Ok(new BaseResponse<IEnumerable<BookViewModel>>(result));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponse<BookDto>>> GetById(int id)
    {
        var response = await _bookService.GetByIdAsync(id);
        if (!response.Success)
            return NotFound(response);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse<BookDto>>> Create([FromBody] CreateBookDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new BaseResponse<BookDto>("Dados inválidos."));

        var response = await _bookService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = response.Data?.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BaseResponse<bool>>> Update(int id, [FromBody] UpdateBookDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new BaseResponse<bool>("Dados inválidos."));

        var response = await _bookService.UpdateAsync(id, dto);
        if (!response.Success)
            return NotFound(response);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<BaseResponse<bool>>> Delete(int id)
    {
        var response = await _bookService.DeleteAsync(id);
        if (!response.Success)
            return NotFound(response);

        return Ok(response);
    }
}
