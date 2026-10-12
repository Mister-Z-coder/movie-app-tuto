using Microsoft.EntityFrameworkCore;
using MovieApp.Models;
namespace MovieApp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext>
options)
        : base(options)
    {
    }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Genre> Genres { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Fluent API : ce que les annotations ne savent pas dire
        modelBuilder.Entity<Genre>()
            .HasIndex(g => g.Nom)
            .IsUnique(); // deux genres ne peuvent pas avoir le même nom
        modelBuilder.Entity<Movie>()
            .HasOne(m => m.Genre)
            .WithMany(g => g.Movies)
            .HasForeignKey(m => m.GenreId)
            .OnDelete(DeleteBehavior.Restrict); // interdit de supprimer un genre qui a des films
        // ===== Données initiales (seed) =====
        modelBuilder.Entity<Genre>().HasData(
            new Genre
            {
                Id = 1,
                Nom = "Science-fiction",
                Description = "Futurs possibles, espace, technologies."
            },
            new Genre
            {
                Id = 2,
                Nom = "Action",
                Description = "Rythme soutenu,combats et poursuites."
            },
            new Genre
            {
                Id = 3,
                Nom = "Thriller",
                Description = "Suspense et tension."
            },
            new Genre 
            {
                Id = 4, 
                Nom = "Animation", 
                Description = "Films d'animation pour tous."
            },
            new Genre
            {
                Id = 5,
                Nom = "Drame",
                Description = "Histoires fortes en émotions."
},
            new Genre
            {
                Id = 6,
                Nom = "Historique",
                Description = "Faits et figures de l'histoire."
            }
        );
        // Les valeurs du seed doivent être FIXES (jamais DateTime.Now),
        // sinon chaque nouvelle migration croit que les données ont changé.
        var dateAjout = new DateTime(2026, 10, 1);
        modelBuilder.Entity<Movie>().HasData(
            new Movie
            {
                Id = 1,
                Titre = "Inception",
                Realisateur = "Christopher Nolan",
                Synopsis = "Un voleur spécialisé dans l'extraction de secrets au cœur des rêves reçoit une mission inverse : implanter une idée.",
                DateSortie = new DateTime(2010, 7, 16),
                DureeMinutes = 148,
                Note = 9,
                GenreId = 1,
                AfficheUrl = "https://placehold.co/400x600?text=Inception",
                DateAjout = dateAjout
            },
            new Movie
            {
                Id = 2,
                Titre = "Interstellar",
                Realisateur = "Christopher Nolan",
                Synopsis = "Alors que la Terre se meurt, un groupe d'astronautes traverse un trou de ver pour trouver une nouvelle planète.",
                DateSortie = new DateTime(2014, 11, 7),
                DureeMinutes = 169,
                Note = 9,
                GenreId = 1,
                AfficheUrl = "https://placehold.co/400x600? text = Interstellar",
                DateAjout = dateAjout
            },
            new Movie
            {
                Id = 3,
                Titre = "Black Panther",
                Realisateur = "Ryan Coogler",
                Synopsis = "Le nouveau roi du Wakanda doit défendre son trône et son peuple face à un rival venu de l'extérieur.",
                DateSortie = new DateTime(2018, 2, 16),
                DureeMinutes = 134,
                Note = 8,
                GenreId = 2,
                AfficheUrl = "https://placehold.co/400x600?text = Black + Panther",
                DateAjout = dateAjout
            },
            new Movie
            {
                Id = 4,
                Titre = "Parasite",
                Realisateur = "Bong Joonho",
                Synopsis = "Une famille pauvre s'infiltre peu à peu au service d'une famille riche, jusqu'au point de rupture.",
                DateSortie = new DateTime(2019, 5, 30),
                DureeMinutes = 132,
                Note = 9,
                GenreId = 3,
                AfficheUrl = "https://placehold.co/400x600?text=Parasite",
                DateAjout = dateAjout
            },
                new Movie
                {
                    Id = 5,
                    Titre = "Toy Story",
                    Realisateur = "John Lasseter",
                    Synopsis = "Les jouets d'un enfant prennent vie dès qu'ilquitte la pièce; l'arrivée d'un nouveau jouet bouscule tout.",
                    DateSortie = new DateTime(1995, 11, 22),
                    DureeMinutes = 81,
                    Note = 8,
                    GenreId = 4,
                    AfficheUrl = "https://placehold.co/400x600?text=Toy+Story",
                    DateAjout = dateAjout
                },
                new Movie
                {
                    Id = 6,
                    Titre = "Titanic",
                    Realisateur = "James Cameron",
                    Synopsis = "Une histoire d'amour à bord du paquebot réputé insubmersible,lors de sa traversée de 1912.",
                    DateSortie = new DateTime(1997, 12, 19),
                    DureeMinutes = 194,
                    Note = 8,
                    GenreId = 5,
                    AfficheUrl = "https://placehold.co/400x600?text=Titanic",
                    DateAjout = dateAjout
                },
                new Movie
                {
                    Id = 7,
                    Titre = "Lumumba",
                    Realisateur = "Raoul Peck",
                    Synopsis = "Le parcours de Patrice Lumumba, de l'indépendance du Congo en 1960 à son assassinat.",
                    DateSortie = new DateTime(2000, 5, 14),
                    DureeMinutes = 115,
                    Note = 8,
                    GenreId = 6,
                    AfficheUrl = "https://placehold.co/400x600?text=Lumumba",
                    DateAjout = dateAjout
                }
);
    }
}