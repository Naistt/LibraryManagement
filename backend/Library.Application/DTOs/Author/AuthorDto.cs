namespace Library.Application.DTOs.Author;

public class AuthorDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Bio { get; set; }
}
