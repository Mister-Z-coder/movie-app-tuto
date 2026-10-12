namespace MovieApp.Models.ViewModels;
public class MovieDetailsViewModel
{
public Movie Film { get; set; } = null!;
public List<Movie> MemeGenre { get; set; } = new();
// Propriété calculée : logique de présentation hors de la vue
public string DureeFormatee => $"{Film.DureeMinutes / 60} h {Film.DureeMinutes % 60:00}";
}