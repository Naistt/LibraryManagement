using Library.Application.DTOs.Genre;
using Library.Application.Interfaces.Services;
using Library.Application.Responses;
using Library.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class GenreController : ControllerBase
{
    private readonly IGenreService _genreService;

    public GenreController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<IEnumerable<GenreDto>>>> GetAll()
    {
        var response = await _genreService.GetAllAsync();
        return Ok(response);
    }

    [HttpGet("detailed")]
    public async Task<ActionResult<BaseResponse<IEnumerable<GenreDto>>>> GetDetailed()
    {
        var response = await _genreService.GetAllDetailedAsync();
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponse<GenreDto>>> GetById(int id)
    {
        var response = await _genreService.GetByIdAsync(id);
        if (!response.Success)
            return NotFound(response);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse<GenreDto>>> Create([FromBody] CreateGenreDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new BaseResponse<GenreDto>("Dados inválidos."));

        var response = await _genreService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = response.Data?.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BaseResponse<bool>>> Update(int id, [FromBody] UpdateGenreDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new BaseResponse<bool>("Dados inválidos."));

        var response = await _genreService.UpdateAsync(id, dto);
        if (!response.Success)
            return NotFound(response);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<BaseResponse<bool>>> Delete(int id)
    {
        var response = await _genreService.DeleteAsync(id);
        if (!response.Success)
            return NotFound(response);

        return Ok(response);
    }
}
