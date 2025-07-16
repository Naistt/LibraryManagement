namespace Library.Application.DTOs.Genre;

public class GenreDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}
