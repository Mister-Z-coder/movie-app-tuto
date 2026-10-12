using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using MovieApp.Models.Settings;
using MovieApp.Models.ViewModels;
using MovieApp.Services;
namespace MovieApp.Controllers;

using MovieApp.Helpers;

public class MoviesController : Controller
{
    private readonly IMovieService _movieService;
    private readonly ILogger<MoviesController> _logger;
    private readonly MovieAppSettings _settings;
    public MoviesController(IMovieService movieService,
                            ILogger<MoviesController> logger,
                            IOptions<MovieAppSettings> options)
    {
        _movieService = movieService;
        _logger = logger;
        _settings = options.Value;
    }
    // GET /Movies  ou  /Movies?page=2
    // GET /Movies?recherche=nolan&genreId=1&page=2
    public async Task<IActionResult> Index(
        [FromQuery] string? recherche,
        [FromQuery] int? genreId,
        [FromQuery] int page = 1)
    {
        if (!ModelState.IsValid || page < 1)
        {
            // ex : /Movies?page=abc -> la conversion échoue, on repart page 1
            _logger.LogWarning("Paramètres de recherche invalides, retour à la page 1");
            page = 1;
        }
        var taille = _settings.FilmsParPage;
        var (films, total) = await _movieService.RechercherAsync(recherche,
    genreId, page, taille);
        var genres = await _movieService.GetGenresAsync();
        var vm = new MovieIndexViewModel
        {
            Films = films,
            TotalFilms = total,
            PageCourante = page,
            TotalPages = Math.Max(1, (int)Math.Ceiling(total / (double)taille)),
            Recherche = recherche,
            GenreId = genreId,
            Genres = genres
                .Select(g => new SelectListItem(g.Nom, g.Id.ToString()))
                .ToList()
        };
        ViewData["Title"] = "Catalogue";
        return View(vm);
    }

    // GET /films/1/inception   (routage par attributs avec contrainte et segment optionnel)
    [HttpGet("films/{id:int}/{slug?}")]
    public async Task<IActionResult> Details([FromRoute] int id, [FromRoute] string? slug)
    {
        var film = await _movieService.GetByIdAsync(id);
        if (film is null)
        {
            _logger.LogWarning("Film {Id} introuvable", id);
            return NotFound(); // 404
        }
        // URL canonique : /films/1 ou /films/1/mauvais-titre -> redirection vers / films / 1 / inception
        var slugAttendu = film.Titre.ToSlug();
        if (slug != slugAttendu)
        {
            return RedirectToAction(nameof(Details), new
            {
                id,
                slug = slugAttendu
            });
        }
        var vm = new MovieDetailsViewModel
        {
            Film = film,
            MemeGenre = await _movieService.MemeGenreAsync(film.GenreId, film.Id, 3)
        };
        ViewData["Title"] = film.Titre;
        return View(vm);
    }
}
