namespace Library.Application.DTOs.Author;

public class CreateAuthorDto
{
    public string Name { get; set; } = default!;
    public string? Bio { get; set; }
}
