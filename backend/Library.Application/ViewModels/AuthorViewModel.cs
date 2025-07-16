namespace Library.Application.ViewModels;

public class AuthorViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Bio { get; set; }

    public List<string> BookTitles { get; set; } = new();
}
