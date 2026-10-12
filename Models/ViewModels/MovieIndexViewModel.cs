using Microsoft.AspNetCore.Mvc.Rendering;
namespace MovieApp.Models.ViewModels;
// Tout ce dont la page Catalogue a besoin, et rien d'autre
public class MovieIndexViewModel
{
public List<Movie> Films { get; set; } = new();
// Critères de recherche (utilisés à l'Étape 10)
public string? Recherche { get; set; }
public int? GenreId { get; set; }
public List<SelectListItem> Genres { get; set; } = new();
// Pagination
public int PageCourante { get; set; } = 1;
public int TotalPages { get; set; }
public int TotalFilms { get; set; }
    public bool APagePrecedente => PageCourante > 1;
    public bool APageSuivante => PageCourante < TotalPages;
}