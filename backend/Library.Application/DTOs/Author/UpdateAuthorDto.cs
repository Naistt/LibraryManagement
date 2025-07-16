namespace Library.Application.DTOs.Author;

public class UpdateAuthorDto
{
    public string Name { get; set; } = default!;
    public string? Bio { get; set; }
}
