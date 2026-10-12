using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MovieApp.Models.Settings;
using MovieApp.Models.ViewModels;
using MovieApp.Services;
namespace MovieApp.Controllers;

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
    public async Task<IActionResult> Index(int page = 1)
    {
        var taille = _settings.FilmsParPage;
        var (films, total) = await _movieService.RechercherAsync(null, null,
page, taille);
        var vm = new MovieIndexViewModel
        {
            Films = films,
            TotalFilms = total,
            PageCourante = page,
            TotalPages = (int)Math.Ceiling(total / (double)taille)
        };
        ViewData["Title"] = "Catalogue";
        return View(vm); // modèle fortement typé
    }
}