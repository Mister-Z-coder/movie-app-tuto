using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Models;
namespace MovieApp.Services;

public class MovieService : IMovieService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<MovieService> _logger;
    // Le DbContext lui-même est injecté : le service ne fait jamais "new"
    public MovieService(ApplicationDbContext context, ILogger<MovieService>
    logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<(List<Movie> Films, int Total)> RechercherAsync(
    string? recherche, int? genreId, int page, int taille)
    {
        // IQueryable : la requête SQL n'est construite et exécutée qu'au moment de l'appel à ToListAsync() ou CountAsync()
        var requete = _context.Movies
        .Include(m => m.Genre) // jointure avec Genres
         .AsNoTracking() // lecture seule : plus rapide
         .AsQueryable();
        if (!string.IsNullOrWhiteSpace(recherche))
        {
            var terme = recherche.Trim();
            requete = requete.Where(m => m.Titre.Contains(terme) || m.Realisateur.Contains(terme));
        }
        if (genreId is > 0)
        {
            requete = requete.Where(m => m.GenreId == genreId);
        }
        var total = await requete.CountAsync();
        var films = await requete
            .OrderByDescending(m => m.Note)
            .ThenBy(m => m.Titre)
            .Skip((page - 1) * taille)  // pagination côté SQL (OFFSET)
            .Take(taille)               // (FETCH NEXT)
            .ToListAsync();
        return (films, total);
    }
    public Task<List<Movie>> DerniersAjoutsAsync(int nombre) =>
        _context.Movies.Include(m => m.Genre).AsNoTracking()
            .OrderByDescending(m => m.DateAjout).ThenByDescending(m => m.Id)
            .Take(nombre).ToListAsync();
    public Task<Movie?> GetByIdAsync(int id) =>
        _context.Movies.Include(m => m.Genre).AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);
    public Task<List<Genre>> GetGenresAsync() =>
        _context.Genres.AsNoTracking().OrderBy(g => g.Nom).ToListAsync();
    public async Task<Movie> CreerAsync(Movie movie)
    {
        movie.DateAjout = DateTime.Now;
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Film créé : {Titre} (Id {Id})", movie.Titre,
movie.Id);
        return movie;
    }
    public async Task<bool> ModifierAsync(Movie movie)
    {
        var existant = await _context.Movies.FindAsync(movie.Id);
        if (existant is null)
        {
            _logger.LogWarning("Modification impossible : film {Id} introuvable", movie.Id);
            return false;
        }
        // On copie champ par champ : DateAjout et Id ne sont jamais écrasés
        existant.Titre = movie.Titre;
        existant.Realisateur = movie.Realisateur;
        existant.Synopsis = movie.Synopsis;
        existant.DateSortie = movie.DateSortie;
        existant.DureeMinutes = movie.DureeMinutes;
        existant.Note = movie.Note;
        existant.AfficheUrl = movie.AfficheUrl;
        existant.GenreId = movie.GenreId;
        await _context.SaveChangesAsync();
        _logger.LogInformation("Film modifié : {Id}", movie.Id);
        return true;
    }
    public async Task<bool> SupprimerAsync(int id)
    {
        var film = await _context.Movies.FindAsync(id);
        if (film is null) return false;
        _context.Movies.Remove(film);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Film supprimé : {Id}", id);
        return true;
    }
    public Task<List<Movie>> MemeGenreAsync(int genreId, int exclureId, int 
nombre) =>
_context.Movies.Include(m => m.Genre).AsNoTracking()
.Where(m => m.GenreId == genreId && m.Id != exclureId)
.OrderByDescending(m => m.Note)
.Take(nombre)
.ToListAsync();
}