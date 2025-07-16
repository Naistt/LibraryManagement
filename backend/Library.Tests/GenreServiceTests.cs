using FluentAssertions;
using Library.Application.DTOs.Genre;
using Library.Application.Interfaces.Repository;
using Library.Application.Responses;
using Library.Application.Services;
using Library.Domain.Entities;
using Moq;

namespace Library.Application.Tests;

public class GenreServiceTests
{
    private readonly GenreService _genreService;
    private readonly Mock<IGenreRepository> _genreRepositoryMock;

    public GenreServiceTests()
    {
        _genreRepositoryMock = new Mock<IGenreRepository>();
        _genreService = new GenreService(_genreRepositoryMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnListOfGenres()
    {
        var genres = new List<Genre>
        {
            new Genre { Id = 1, Name = "Terror" },
            new Genre { Id = 2, Name = "Aventura" },
            new Genre { Id = 3, Name = "Ação" }
        };

        _genreRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(genres);

        var result = await _genreService.GetAllAsync();

        result.Data.Should().HaveCount(3);
        result.Data.First().Name.Should().Be("Terror");
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnGenre_WhenFound()
    {
        var genre = new Genre { Id = 1, Name = "Ficção" };
        _genreRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(genre);

        var result = await _genreService.GetByIdAsync(1);

        result.Data.Should().NotBeNull();
        result.Data!.Name.Should().Be("Ficção");
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNotFound_WhenMissing()
    {
        _genreRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Genre?)null);

        var result = await _genreService.GetByIdAsync(999);

        result.Success.Should().BeFalse();
        result.Data.Should().BeNull();
        result.Message.Should().Contain("não encontrado");
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnCreatedGenre()
    {
        var dto = new CreateGenreDto { Name = "Drama", Description = "Drama description" };

        var result = await _genreService.CreateAsync(dto);

        result.Success.Should().BeTrue();
        result.Data!.Name.Should().Be("Drama");
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnTrue_WhenGenreExists()
    {
        var genre = new Genre { Id = 1, Name = "Old" };
        _genreRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(genre);

        var dto = new UpdateGenreDto { Name = "New", Description = "Updated" };

        var result = await _genreService.UpdateAsync(1, dto);

        result.Success.Should().BeTrue();
        genre.Name.Should().Be("New");
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnFalse_WhenNotFound()
    {
        _genreRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Genre?)null);

        var dto = new UpdateGenreDto { Name = "None", Description = "Nada" };

        var result = await _genreService.UpdateAsync(99, dto);

        result.Success.Should().BeFalse();
        result.Message.Should().Contain("não encontrado");
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_WhenExists()
    {
        var genre = new Genre { Id = 1, Name = "To delete" };
        _genreRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(genre);

        var result = await _genreService.DeleteAsync(1);

        result.Success.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_WhenNotExists()
    {
        _genreRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((Genre?)null);

        var result = await _genreService.DeleteAsync(1);

        result.Success.Should().BeFalse();
        result.Message.Should().Contain("não encontrado");
    }
}
