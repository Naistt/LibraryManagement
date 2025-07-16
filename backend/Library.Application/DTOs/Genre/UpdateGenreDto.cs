namespace Library.Application.DTOs.Genre;

public class UpdateGenreDto
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}
