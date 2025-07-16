using FluentAssertions;
using Library.Application.DTOs.Book;
using Library.Application.Interfaces.Repository;
using Library.Application.Responses;
using Library.Application.Services;
using Library.Domain.Entities;
using Moq;

namespace Library.Application.Tests;

public class BookServiceTests
{
    private readonly BookService _bookService;
    private readonly Mock<IBookRepository> _bookRepositoryMock;

    public BookServiceTests()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _bookService = new BookService(_bookRepositoryMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnBooksAsDto()
    {
        var books = new List<Book>
        {
            new Book { Id = 1, Title = "Dom Casmurro", AuthorId = 1, GenreId = 1 },
            new Book { Id = 2, Title = "A Hora da Estrela", AuthorId = 2, GenreId = 2 }
        };

        _bookRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(books);

        BaseResponse<IEnumerable<BookDto>> response = await _bookService.GetAllAsync();

        response.Should().NotBeNull();
        response.Success.Should().BeTrue();
        response.Data.Should().HaveCount(2);
        response.Data.First().Title.Should().Be("Dom Casmurro");
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnBook_WhenFound()
    {
        var book = new Book { Id = 1, Title = "Livro Teste", AuthorId = 1, GenreId = 1 };
        _bookRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(book);

        var result = await _bookService.GetByIdAsync(1);

        result.Data.Should().NotBeNull();
        result.Data!.Title.Should().Be("Livro Teste");
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNotFound_WhenMissing()
    {
        _bookRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Book?)null);

        var result = await _bookService.GetByIdAsync(999);

        result.Success.Should().BeFalse();
        result.Data.Should().BeNull();
        result.Message.Should().Contain("não encontrado");
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnCreatedBook()
    {
        var dto = new CreateBookDto { Title = "Novo Livro", AuthorId = 1, GenreId = 1, Summary = "Resumo", PublicationYear = 2020 };

        var result = await _bookService.CreateAsync(dto);

        result.Success.Should().BeTrue();
        result.Data!.Title.Should().Be("Novo Livro");
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnTrue_WhenBookExists()
    {
        var book = new Book { Id = 1, Title = "Old" };
        _bookRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(book);

        var dto = new UpdateBookDto { Title = "New", Summary = "Updated", PublicationYear = 2021, AuthorId = 1, GenreId = 1 };

        var result = await _bookService.UpdateAsync(1, dto);

        result.Success.Should().BeTrue();
        book.Title.Should().Be("New");
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnFalse_WhenNotFound()
    {
        _bookRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Book?)null);

        var dto = new UpdateBookDto { Title = "None", Summary = "Nada", PublicationYear = 2021, AuthorId = 1, GenreId = 1 };

        var result = await _bookService.UpdateAsync(99, dto);

        result.Success.Should().BeFalse();
        result.Message.Should().Contain("não encontrado");
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_WhenExists()
    {
        var book = new Book { Id = 1, Title = "To delete" };
        _bookRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(book);

        var result = await _bookService.DeleteAsync(1);

        result.Success.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_WhenNotExists()
    {
        _bookRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((Book?)null);

        var result = await _bookService.DeleteAsync(1);

        result.Success.Should().BeFalse();
        result.Message.Should().Contain("não encontrado");
    }
}
