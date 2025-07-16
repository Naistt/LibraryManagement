using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Book> Books => Set<Book>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("library");
        base.OnModelCreating(modelBuilder);

        // Genre mapping
        modelBuilder.Entity<Genre>(entity =>
        {
            entity.ToTable("genres", "library");

            entity.HasKey(g => g.Id);
            entity.Property(g => g.Name).IsRequired().HasMaxLength(100);
            entity.Property(g => g.Description).HasMaxLength(500);
        });

        // Author mapping
        modelBuilder.Entity<Author>(entity =>
        {
            entity.ToTable("authors", "library");

            entity.HasKey(a => a.Id);
            entity.Property(a => a.Name).IsRequired().HasMaxLength(150);
            entity.Property(a => a.Bio).HasMaxLength(1000);
        });

        // Book mapping
        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("books", "library");

            entity.HasKey(b => b.Id);
            entity.Property(b => b.Title).IsRequired().HasMaxLength(200);
            entity.Property(b => b.Summary).HasMaxLength(2000);
            entity.Property(b => b.PublicationYear);

            entity.HasOne(b => b.Genre)
                  .WithMany(g => g.Books)
                  .HasForeignKey(b => b.GenreId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(b => b.Author)
                  .WithMany(a => a.Books)
                  .HasForeignKey(b => b.AuthorId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
