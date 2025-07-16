namespace Library.Application.DTOs.Book;

public class CreateBookDto
{
    public string Title { get; set; } = default!;
    public string? Summary { get; set; }
    public int PublicationYear { get; set; }

    public int GenreId { get; set; }
    public int AuthorId { get; set; }
}
