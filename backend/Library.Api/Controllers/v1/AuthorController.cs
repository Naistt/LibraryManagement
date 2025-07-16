using Microsoft.AspNetCore.Mvc;
using Library.Application.DTOs.Author;
using Library.Application.Interfaces.Services;
using Library.Application.Responses;
using Library.Application.ViewModels;

namespace Library.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<IEnumerable<AuthorDto>>>> GetAll()
    {
        var response = await _authorService.GetAllAsync();
        return Ok(response);
    }

    [HttpGet("detailed")]
    public async Task<ActionResult<BaseResponse<IEnumerable<AuthorDto>>>> GetDetailed()
    {
        var response = await _authorService.GetAllDetailedAsync();
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponse<AuthorDto>>> GetById(int id)
    {
        var response = await _authorService.GetByIdAsync(id);
        if (!response.Success)
            return NotFound(response);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse<AuthorDto>>> Create([FromBody] CreateAuthorDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new BaseResponse<AuthorDto>("Dados inválidos."));

        var response = await _authorService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = response.Data?.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BaseResponse<bool>>> Update(int id, [FromBody] UpdateAuthorDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new BaseResponse<bool>("Dados inválidos."));

        var response = await _authorService.UpdateAsync(id, dto);
        if (!response.Success)
            return NotFound(response);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<BaseResponse<bool>>> Delete(int id)
    {
        var response = await _authorService.DeleteAsync(id);
        if (!response.Success)
            return NotFound(response);

        return Ok(response);
    }
}

