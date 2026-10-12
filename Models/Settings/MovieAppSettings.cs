namespace MovieApp.Models.Settings;
// Les noms des propriétés doivent correspondre aux clés JSON
public class MovieAppSettings
{
    public string NomApplication { get; set; } = "MovieApp";
    public string Slogan { get; set; } = string.Empty;
    public int FilmsParPage { get; set; } = 6;
    public int SeuilRequeteLenteMs { get; set; } = 500;
}