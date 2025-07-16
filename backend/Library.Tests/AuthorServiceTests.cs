using FluentAssertions;
using Library.Application.DTOs.Author;
using Library.Application.Interfaces.Repository;
using Library.Application.Responses;
using Library.Application.Services;
using Library.Domain.Entities;
using Moq;

namespace Library.Application.Tests;

public class AuthorServiceTests
{
    private readonly AuthorService _authorService;
    private readonly Mock<IAuthorRepository> _authorRepositoryMock;

    public AuthorServiceTests()
    {
        _authorRepositoryMock = new Mock<IAuthorRepository>();
        _authorService = new AuthorService(_authorRepositoryMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAuthorsAsDto()
    {
        var authors = new List<Author>
        {
            new Author { Id = 1, Name = "Machado de Assis", Bio = "Escritor brasileiro" },
            new Author { Id = 2, Name = "Clarice Lispector", Bio = "Autora renomada" }
        };

        _authorRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(authors);

        var result = await _authorService.GetAllAsync();

        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Data.Should().HaveCount(2);
        result.Data.First().Name.Should().Be("Machado de Assis");
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnAuthor_WhenFound()
    {
        var author = new Author { Id = 1, Name = "Autor Teste", Bio = "Bio" };
        _authorRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(author);

        var result = await _authorService.GetByIdAsync(1);

        result.Data.Should().NotBeNull();
        result.Data!.Name.Should().Be("Autor Teste");
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNotFound_WhenMissing()
    {
        _authorRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Author?)null);

        var result = await _authorService.GetByIdAsync(999);

        result.Success.Should().BeFalse();
        result.Data.Should().BeNull();
        result.Message.Should().Contain("não encontrado");
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnCreatedAuthor()
    {
        var dto = new CreateAuthorDto { Name = "Novo Autor", Bio = "Bio do autor" };

        var result = await _authorService.CreateAsync(dto);

        result.Success.Should().BeTrue();
        result.Data!.Name.Should().Be("Novo Autor");
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnTrue_WhenAuthorExists()
    {
        var author = new Author { Id = 1, Name = "Old" };
        _authorRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(author);

        var dto = new UpdateAuthorDto { Name = "New", Bio = "Updated" };

        var result = await _authorService.UpdateAsync(1, dto);

        result.Success.Should().BeTrue();
        author.Name.Should().Be("New");
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnFalse_WhenNotFound()
    {
        _authorRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Author?)null);

        var dto = new UpdateAuthorDto { Name = "None", Bio = "Nada" };

        var result = await _authorService.UpdateAsync(99, dto);

        result.Success.Should().BeFalse();
        result.Message.Should().Contain("não encontrado");
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_WhenExists()
    {
        var author = new Author { Id = 1, Name = "To delete" };
        _authorRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(author);

        var result = await _authorService.DeleteAsync(1);

        result.Success.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_WhenNotExists()
    {
        _authorRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((Author?)null);

        var result = await _authorService.DeleteAsync(1);

        result.Success.Should().BeFalse();
        result.Message.Should().Contain("não encontrado");
    }
}
