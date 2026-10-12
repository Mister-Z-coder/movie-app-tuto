using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MovieApp.Models;
using MovieApp.Models.Settings;
using MovieApp.Services;

namespace MovieApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly MovieAppSettings _settings;
    // Injection de dépendances par le constructeur (détaillée à l'Étape 8)
    private readonly IMovieService _movieService;
    public HomeController(ILogger<HomeController> logger,
    IOptions<MovieAppSettings> options,
    IMovieService movieService)
    {
        _logger = logger;
        _settings = options.Value;
        _movieService = movieService;
    }
    public async Task<IActionResult> Index()
    {
        // ... ViewData / ViewBag de l'Étape 5 inchangés ...
        var derniers = await _movieService.DerniersAjoutsAsync(3);
        return View(derniers); // la vue devient fortement typée
    }
    
    // GET /Home/Bonjour?nom=Jeremie  -> simple chaîne
    public string Bonjour(string nom = "étudiant")
    {
        return $"Bonjour {nom}, bienvenue dans {_settings.NomApplication} !";
    }
    // GET /Home/Heure  -> ContentResult
    public IActionResult Heure()
    {
        return Content($"Heure du serveur : {DateTime.Now:HH:mm:ss}");
    }
    // GET /Home/Infos  -> JsonResult
    public IActionResult Infos()
    {
        var infos = new
        {
            Application = _settings.NomApplication,
            Version = "1.0",
            FilmsParPage = _settings.FilmsParPage
        };
        return Json(infos);
    }
    // GET /Home/Documentation  -> RedirectResult (URL externe)
    public IActionResult Documentation()
    {
        return Redirect("https://learn.microsoft.com/aspnet/core/mvc/overview");
    }
    // GET /Home/Accueil  -> RedirectToActionResult (action interne)
    public IActionResult Accueil()
    {
        return RedirectToAction(nameof(Index));
    }
    // Routage par attributs : uniquement accessible via /a-propos
    [Route("a-propos")]
    public IActionResult About()
    {
        return View();
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }

}
