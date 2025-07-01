using MovieAPI.Models;

namespace MovieAPI.Data
{
    public static class SeedData
    {
        public static async Task Initialize(MovieDbContext context)
        {
            if (context.Movies.Any())
                return;

            var movies = new List<Movie>
            {
                new Movie
                {
                    Title = "El Señor de los Anillos: La Comunidad del Anillo",
                    Description = "Un hobbit emprende un épico viaje para destruir un anillo poderoso y salvar a la Tierra Media del Señor Oscuro Sauron.",
                    Year = 2001,
                    Genre = "Fantasía",
                    Director = "Peter Jackson",
                    Cast = "Elijah Wood, Ian McKellen, Viggo Mortensen",
                    Rating = 8.8m,
                    PosterUrl = "https://images.unsplash.com/photo-1536440136628-849c177e76a1?w=400",
                    TrailerUrl = "https://www.youtube.com/watch?v=V75dMMIW2B4",
                    VideoUrl = "https://sample-videos.com/zip/10/mp4/SampleVideo_1280x720_1mb.mp4",
                    Duration = 178,
                    Language = "Español",
                    IsFeatured = true,
                    IsNew = false
                },
                new Movie
                {
                    Title = "Inception",
                    Description = "Un ladrón experto en el arte de la extracción roba información valiosa de las mentes de las personas mientras sueñan.",
                    Year = 2010,
                    Genre = "Ciencia Ficción",
                    Director = "Christopher Nolan",
                    Cast = "Leonardo DiCaprio, Joseph Gordon-Levitt, Ellen Page",
                    Rating = 8.8m,
                    PosterUrl = "https://images.unsplash.com/photo-1489599849927-2ee91cede3ba?w=400",
                    TrailerUrl = "https://www.youtube.com/watch?v=YoHD9XEInc0",
                    VideoUrl = "https://sample-videos.com/zip/10/mp4/SampleVideo_1280x720_1mb.mp4",
                    Duration = 148,
                    Language = "Español",
                    IsFeatured = true,
                    IsNew = false
                },
                new Movie
                {
                    Title = "Pulp Fiction",
                    Description = "Las vidas de dos sicarios, un boxeador, la esposa de un gángster y dos bandidos se entrelazan en cuatro historias de violencia y redención.",
                    Year = 1994,
                    Genre = "Crimen",
                    Director = "Quentin Tarantino",
                    Cast = "John Travolta, Uma Thurman, Samuel L. Jackson",
                    Rating = 8.9m,
                    PosterUrl = "https://images.unsplash.com/photo-1489599849927-2ee91cede3ba?w=400",
                    TrailerUrl = "https://www.youtube.com/watch?v=s7EdQ4FqbhY",
                    VideoUrl = "https://sample-videos.com/zip/10/mp4/SampleVideo_1280x720_1mb.mp4",
                    Duration = 154,
                    Language = "Español",
                    IsFeatured = false,
                    IsNew = false
                },
                new Movie
                {
                    Title = "The Dark Knight",
                    Description = "Batman se enfrenta al caos que el Joker desata en Gotham City, mientras lucha con sus propios demonios internos.",
                    Year = 2008,
                    Genre = "Acción",
                    Director = "Christopher Nolan",
                    Cast = "Christian Bale, Heath Ledger, Aaron Eckhart",
                    Rating = 9.0m,
                    PosterUrl = "https://images.unsplash.com/photo-1536440136628-849c177e76a1?w=400",
                    TrailerUrl = "https://www.youtube.com/watch?v=EXeTwQWrcwY",
                    VideoUrl = "https://sample-videos.com/zip/10/mp4/SampleVideo_1280x720_1mb.mp4",
                    Duration = 152,
                    Language = "Español",
                    IsFeatured = true,
                    IsNew = false
                },
                new Movie
                {
                    Title = "Fight Club",
                    Description = "Un empleado de oficina insomne y un vendedor de jabón forman un club de lucha clandestino que evoluciona hacia algo mucho más grande.",
                    Year = 1999,
                    Genre = "Drama",
                    Director = "David Fincher",
                    Cast = "Brad Pitt, Edward Norton, Helena Bonham Carter",
                    Rating = 8.8m,
                    PosterUrl = "https://images.unsplash.com/photo-1489599849927-2ee91cede3ba?w=400",
                    TrailerUrl = "https://www.youtube.com/watch?v=SUXWAEX2jlg",
                    VideoUrl = "https://sample-videos.com/zip/10/mp4/SampleVideo_1280x720_1mb.mp4",
                    Duration = 139,
                    Language = "Español",
                    IsFeatured = false,
                    IsNew = false
                },
                new Movie
                {
                    Title = "Forrest Gump",
                    Description = "La vida de Forrest Gump, un hombre con un coeficiente intelectual bajo, que logra grandes cosas en la vida sin darse cuenta.",
                    Year = 1994,
                    Genre = "Drama",
                    Director = "Robert Zemeckis",
                    Cast = "Tom Hanks, Robin Wright, Gary Sinise",
                    Rating = 8.8m,
                    PosterUrl = "https://images.unsplash.com/photo-1536440136628-849c177e76a1?w=400",
                    TrailerUrl = "https://www.youtube.com/watch?v=bLvqoHBptjg",
                    VideoUrl = "https://sample-videos.com/zip/10/mp4/SampleVideo_1280x720_1mb.mp4",
                    Duration = 142,
                    Language = "Español",
                    IsFeatured = false,
                    IsNew = false
                },
                new Movie
                {
                    Title = "The Matrix",
                    Description = "Un programador descubre que la realidad tal como la conocemos es una simulación creada por máquinas, y se une a una rebelión para liberar a la humanidad.",
                    Year = 1999,
                    Genre = "Ciencia Ficción",
                    Director = "Lana y Lilly Wachowski",
                    Cast = "Keanu Reeves, Laurence Fishburne, Carrie-Anne Moss",
                    Rating = 8.7m,
                    PosterUrl = "https://images.unsplash.com/photo-1489599849927-2ee91cede3ba?w=400",
                    TrailerUrl = "https://www.youtube.com/watch?v=m8e-FF8MsqU",
                    VideoUrl = "https://sample-videos.com/zip/10/mp4/SampleVideo_1280x720_1mb.mp4",
                    Duration = 136,
                    Language = "Español",
                    IsFeatured = true,
                    IsNew = false
                },
                new Movie
                {
                    Title = "Goodfellas",
                    Description = "La historia de Henry Hill y su vida en la mafia, desde su juventud hasta convertirse en un informante del FBI.",
                    Year = 1990,
                    Genre = "Crimen",
                    Director = "Martin Scorsese",
                    Cast = "Robert De Niro, Ray Liotta, Joe Pesci",
                    Rating = 8.7m,
                    PosterUrl = "https://images.unsplash.com/photo-1536440136628-849c177e76a1?w=400",
                    TrailerUrl = "https://www.youtube.com/watch?v=qo5jJpHtI1Y",
                    VideoUrl = "https://sample-videos.com/zip/10/mp4/SampleVideo_1280x720_1mb.mp4",
                    Duration = 146,
                    Language = "Español",
                    IsFeatured = false,
                    IsNew = false
                },
                new Movie
                {
                    Title = "Interstellar",
                    Description = "Un equipo de exploradores viaja a través de un agujero de gusano en el espacio en un intento de asegurar la supervivencia de la humanidad.",
                    Year = 2014,
                    Genre = "Ciencia Ficción",
                    Director = "Christopher Nolan",
                    Cast = "Matthew McConaughey, Anne Hathaway, Jessica Chastain",
                    Rating = 8.6m,
                    PosterUrl = "https://images.unsplash.com/photo-1489599849927-2ee91cede3ba?w=400",
                    TrailerUrl = "https://www.youtube.com/watch?v=2LqzF5WauAw",
                    VideoUrl = "https://sample-videos.com/zip/10/mp4/SampleVideo_1280x720_1mb.mp4",
                    Duration = 169,
                    Language = "Español",
                    IsFeatured = true,
                    IsNew = false
                },
                new Movie
                {
                    Title = "The Silence of the Lambs",
                    Description = "Una joven agente del FBI debe confiar en la ayuda de un asesino en serie encarcelado para atrapar a otro asesino que mata a sus víctimas.",
                    Year = 1991,
                    Genre = "Thriller",
                    Director = "Jonathan Demme",
                    Cast = "Jodie Foster, Anthony Hopkins, Scott Glenn",
                    Rating = 8.6m,
                    PosterUrl = "https://images.unsplash.com/photo-1536440136628-849c177e76a1?w=400",
                    TrailerUrl = "https://www.youtube.com/watch?v=W6Mm8Sbe__o",
                    VideoUrl = "https://sample-videos.com/zip/10/mp4/SampleVideo_1280x720_1mb.mp4",
                    Duration = 118,
                    Language = "Español",
                    IsFeatured = false,
                    IsNew = false
                }
            };

            context.Movies.AddRange(movies);
            await context.SaveChangesAsync();

            // Crear usuario administrador por defecto
            if (!context.Users.Any())
            {
                var adminUser = new User
                {
                    Username = "admin",
                    Email = "admin@movieapi.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    FirstName = "Administrador",
                    LastName = "Sistema",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    Role = "Admin",
                    IsActive = true
                };

                context.Users.Add(adminUser);
                await context.SaveChangesAsync();
            }
        }
    }
} 