using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MovieApp.Models;
using MovieApp.Models.Settings;

namespace MovieApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly MovieAppSettings _settings;
    // Injection de dépendances par le constructeur (détaillée à l'Étape 8)
    public HomeController(ILogger<HomeController> logger,
                          IOptions<MovieAppSettings> options)
    {
        _logger = logger;
        _settings = options.Value;
    }
    // GET /  ou  /Home  ou  /Home/Index  -> ViewResult
    public IActionResult Index()
    {
        _logger.LogInformation("Affichage de la page d'accueil");
        ViewData["Title"] = "Accueil";
        ViewBag.Slogan = _settings.Slogan;
        // ViewData : clé/valeur
        // ViewBag : propriété dynamique
        ViewBag.Fonctionnalites = new List<string>
{
"Catalogue avec recherche, filtre et pagination",
"Fiche détaillée de chaque film",
"Ajout, modification et suppression de films",
"API REST au format JSON"
};
        return View();
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
