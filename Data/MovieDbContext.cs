using Microsoft.EntityFrameworkCore;
using MovieAPI.Models;

namespace MovieAPI.Data
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserFavorite> UserFavorites { get; set; }
        public DbSet<WatchHistory> WatchHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de Movie
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.Genre).HasMaxLength(50);
                entity.Property(e => e.Director).HasMaxLength(100);
                entity.Property(e => e.Cast).HasMaxLength(500);
                entity.Property(e => e.PosterUrl).HasMaxLength(500);
                entity.Property(e => e.TrailerUrl).HasMaxLength(500);
                entity.Property(e => e.VideoUrl).HasMaxLength(500);
                entity.Property(e => e.Language).HasMaxLength(50);
                entity.Property(e => e.Rating).HasPrecision(3, 1);
            });

            // Configuración de User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.FirstName).HasMaxLength(100);
                entity.Property(e => e.LastName).HasMaxLength(100);
                entity.Property(e => e.Role).HasMaxLength(20);
                
                // Índices únicos
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // Configuración de UserFavorite
            modelBuilder.Entity<UserFavorite>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.User)
                    .WithMany(u => u.UserFavorites)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Movie)
                    .WithMany(m => m.UserFavorites)
                    .HasForeignKey(e => e.MovieId)
                    .OnDelete(DeleteBehavior.Cascade);
                
                // Índice único para evitar duplicados
                entity.HasIndex(e => new { e.UserId, e.MovieId }).IsUnique();
            });

            // Configuración de WatchHistory
            modelBuilder.Entity<WatchHistory>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.User)
                    .WithMany(u => u.WatchHistories)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Movie)
                    .WithMany(m => m.WatchHistories)
                    .HasForeignKey(e => e.MovieId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
} 