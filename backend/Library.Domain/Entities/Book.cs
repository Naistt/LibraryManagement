namespace Library.Domain.Entities;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Summary { get; set; }
    public int PublicationYear { get; set; }

    public int GenreId { get; set; }
    public Genre Genre { get; set; } = default!;

    public int AuthorId { get; set; }
    public Author Author { get; set; } = default!;
}
