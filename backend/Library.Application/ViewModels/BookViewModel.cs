namespace Library.Application.ViewModels;

public class BookViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Summary { get; set; }
    public int PublicationYear { get; set; }

    public int AuthorId { get; set; }
    public string AuthorName { get; set; } = string.Empty;

    public int GenreId { get; set; }
    public string GenreName { get; set; } = string.Empty;
}
