using MovieApp.Models;
namespace MovieApp.Services;
public interface IMovieService
{
Task<(List<Movie> Films, int Total)> RechercherAsync(string? recherche, 
int? genreId, int page, int taille);
Task<List<Movie>> DerniersAjoutsAsync(int nombre);
Task<Movie?> GetByIdAsync(int id);
Task<List<Genre>> GetGenresAsync();
Task<Movie> CreerAsync(Movie movie);
Task<bool> ModifierAsync(Movie movie);
Task<bool> SupprimerAsync(int id);
}