namespace Library.Application.DTOs.Genre;

public class CreateGenreDto
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}
