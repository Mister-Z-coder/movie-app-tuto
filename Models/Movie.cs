using System.ComponentModel.DataAnnotations;
using MovieApp.Models.Validation;
namespace MovieApp.Models;

public class Movie
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Le titre est obligatoire.")]
    [StringLength(150, ErrorMessage = "Le titre ne peut pas dépasser 150 caractères.")]
    public string Titre { get; set; } = string.Empty;
    [Required(ErrorMessage = "Le réalisateur est obligatoire.")]
    [StringLength(100)]
    [Display(Name = "Réalisateur")]
    public string Realisateur { get; set; } = string.Empty;
    [StringLength(2000)]
    [DataType(DataType.MultilineText)]
    public string? Synopsis { get; set; }
    [Required(ErrorMessage = "La date de sortie est obligatoire.")]
    [DataType(DataType.Date)]
    [DateSortieValide]
    [Display(Name = "Date de sortie")]
    public DateTime DateSortie { get; set; }
    [Range(1, 600, ErrorMessage = "La durée doit être comprise entre 1 et 600 minutes.")]
    [Display(Name = "Durée (min)")]
    public int DureeMinutes { get; set; }
    [Range(1, 10, ErrorMessage = "La note doit être comprise entre 1 et 10.")]
    [Display(Name = "Note /10")]
    public int Note { get; set; }
    [Url(ErrorMessage = "Adresse web invalide.")]
    [StringLength(500)]
    [Display(Name = "URL de l'affiche")]
    public string? AfficheUrl { get; set; }
    [Display(Name = "Ajouté le")]
    public DateTime DateAjout { get; set; } = DateTime.Now;

    // Clé étrangère + navigation vers le genre
    [Range(1, int.MaxValue, ErrorMessage = "Choisissez un genre.")]
    [Display(Name = "Genre")]
    public int GenreId { get; set; }
    public Genre? Genre { get; set; }
}