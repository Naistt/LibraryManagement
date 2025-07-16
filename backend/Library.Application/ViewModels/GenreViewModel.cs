namespace Library.Application.ViewModels;

public class GenreViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public List<string> BookTitles { get; set; } = new();
}
