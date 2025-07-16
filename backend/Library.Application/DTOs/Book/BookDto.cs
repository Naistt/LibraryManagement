namespace Library.Application.DTOs.Book;

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Summary { get; set; }
    public int PublicationYear { get; set; }

    public int GenreId { get; set; }
    public string GenreName { get; set; } = default!;

    public int AuthorId { get; set; }
    public string AuthorName { get; set; } = default!;
}
